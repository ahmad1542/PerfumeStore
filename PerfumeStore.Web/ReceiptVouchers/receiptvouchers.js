function setTableHeader() {
  const thead = $('tableHead');
  if (!thead) return;
  thead.innerHTML = `<tr><th>#</th><th>Date</th><th>Party</th><th>Type</th><th>Amount</th><th>Applied Count</th><th>Money Account</th><th style="text-align:right;">Actions</th></tr>`;
}

async function loadList(searchText = '') {
  setTableHeader();

  const tbody = $('rows');
  const emptyState = $('emptyState');
  const tableCard = $('tableCard');
  const countChip = $('countChip');

  if (!tbody) return;

  setMsg('pageMsg', 'Loading...', false);

  const params = new URLSearchParams();
  if (searchText && searchText.trim().length > 0) params.append('search', searchText.trim());

  try {
    const list = await apiGetJson(params.toString() ? `${API}?${params.toString()}` : API);
    const items = Array.isArray(list) ? list : [];

    tbody.innerHTML = '';
    if (countChip) countChip.textContent = items.length;

    if (items.length === 0) {
      if (emptyState) emptyState.style.display = '';
      if (tableCard) tableCard.style.display = 'none';
      setMsg('pageMsg', '', false);
      return;
    }

    if (emptyState) emptyState.style.display = 'none';
    if (tableCard) tableCard.style.display = '';

    items.forEach(item => {
      const id = item.id ?? item.ID ?? '';
      const partyName = item.partyName ?? item.PartyName ?? '-';
      const type = item.receiptForType ?? item.ReceiptForType ?? 'customer';
      const amount = Number(item.amount ?? item.Amount ?? 0).toFixed(2);
      const appliedCount = Number(item.appliedInvoicesCount ?? item.AppliedInvoicesCount ?? 0) + Number(item.appliedDebtsCount ?? item.AppliedDebtsCount ?? 0);
      const moneyAccountName = item.moneyAccountName ?? item.MoneyAccountName ?? '-';
      const notes = item.notes ?? item.Notes ?? '';
      const date = formatDateOnly(item.date ?? item.Date ?? '');

      const tr = document.createElement('tr');
      tr.innerHTML = `
        <td>#${escapeHtml(id)}</td>
        <td>${escapeHtml(date)}</td>
        <td>${escapeHtml(partyName)}</td>
        <td>${escapeHtml(type === 'person' ? 'Person Debt' : 'Customer')}</td>
        <td>${escapeHtml(amount)}</td>
        <td>${escapeHtml(String(appliedCount))}</td>
        <td>${escapeHtml(moneyAccountName)}</td>
        <td style="text-align:right; white-space:nowrap;">
          <a class="btn-light" href="view.html?id=${encodeURIComponent(id)}">View</a>
        </td>
      `;
      tr.title = notes || '';
      tbody.appendChild(tr);
    });

    setMsg('pageMsg', '', false);
  } catch (e) {
    if (emptyState) emptyState.style.display = 'none';
    if (tableCard) tableCard.style.display = 'none';
    setMsg('pageMsg', getFriendlyMessage(e), true);
  }
}

document.getElementById('searchInput')?.addEventListener('input', e => loadList(e.target.value));
