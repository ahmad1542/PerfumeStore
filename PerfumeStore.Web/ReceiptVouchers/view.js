async function loadView() {
  const id = getQueryParam('id');
  if (!id) {
    setMsg('pageMsg', 'Missing id in URL.', true);
    return;
  }

  setMsg('pageMsg', 'Loading...', false);

  try {
    const item = await apiGetJson(`${API}/${encodeURIComponent(id)}`);
    const detailGrid = $('detailGrid');
    const applications = Array.isArray(item.applications ?? item.Applications) ? (item.applications ?? item.Applications) : [];
    const receiptType = (item.receiptForType ?? item.ReceiptForType) === 'person' ? 'Person Debt' : 'Customer';

    detailGrid.innerHTML = `
      <div class="detail-card"><span class="detail-label">Voucher #</span><strong>${escapeHtml(item.id ?? item.ID ?? '')}</strong></div>
      <div class="detail-card"><span class="detail-label">Date</span><strong>${escapeHtml(formatDateOnly(item.date ?? item.Date ?? ''))}</strong></div>
      <div class="detail-card"><span class="detail-label">Party</span><strong>${escapeHtml(item.partyName ?? item.PartyName ?? '-')}</strong></div>
      <div class="detail-card"><span class="detail-label">Receipt Type</span><strong>${escapeHtml(receiptType)}</strong></div>
      <div class="detail-card"><span class="detail-label">Money Account</span><strong>${escapeHtml(item.moneyAccountName ?? item.MoneyAccountName ?? '-')}</strong></div>
      <div class="detail-card"><span class="detail-label">Amount</span><strong>${escapeHtml(String(item.amount ?? item.Amount ?? 0))}</strong></div>
      <div class="detail-card detail-card-wide"><span class="detail-label">Notes</span><strong>${escapeHtml(item.notes ?? item.Notes ?? '-')}</strong></div>
    `;

    const empty = $('applicationsEmpty');
    const card = $('applicationsCard');
    const rows = $('applicationRows');
    rows.innerHTML = '';

    if (applications.length === 0) {
      if (empty) empty.style.display = '';
      if (card) card.style.display = 'none';
    } else {
      applications.forEach(x => {
        const type = x.applicationType ?? x.ApplicationType;
        const tr = document.createElement('tr');
        tr.innerHTML = `
          <td>${escapeHtml(type === 'personDebt' ? `Debt #${x.debtId ?? x.DebtId ?? ''}` : `Invoice #${x.salesInvoiceId ?? x.SalesInvoiceId ?? ''}`)}</td>
          <td>${escapeHtml(type === 'personDebt' ? 'Person Debt' : 'Sales Invoice')}</td>
          <td>${escapeHtml(formatDateOnly(x.itemDate ?? x.ItemDate ?? ''))}</td>
          <td>${escapeHtml(x.notes ?? x.Notes ?? '-')}</td>
          <td>${escapeHtml(String(x.appliedAmount ?? x.AppliedAmount ?? 0))}</td>
        `;
        rows.appendChild(tr);
      });
      if (empty) empty.style.display = 'none';
      if (card) card.style.display = '';
    }

    setMsg('pageMsg', '');
  } catch (e) {
    setMsg('pageMsg', 'Failed to load receipt voucher.', true);
    console.error(e);
  }
}
