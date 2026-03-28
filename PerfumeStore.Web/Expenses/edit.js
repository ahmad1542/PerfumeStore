async function initEdit() {
  const id = getQueryParam('id');
  if (!id) {
    setMsg('pageMsg', 'Missing id in URL.', true);
    return;
  }

  try {
    await fillMoneyAccounts('MoneyAccountID');
    await fillExpenseTypes();
    wireExpenseTypeCreation();

    setMsg('pageMsg', 'Loading...', false);
    const item = await apiGetJson(`${API}/${encodeURIComponent(id)}`);

    if ($('Date')) $('Date').value = formatDateOnly(item.date ?? item.Date ?? '');
    if ($('Amount')) $('Amount').value = item.amount ?? item.Amount ?? '';
    if ($('ExpenseTypeID')) $('ExpenseTypeID').value = String(item.expenseTypeId ?? item.ExpenseTypeId ?? 0);
    if ($('MoneyAccountID')) $('MoneyAccountID').value = String(item.moneyAccountID ?? item.MoneyAccountID ?? 0);
    if ($('Notes')) $('Notes').value = item.notes ?? item.Notes ?? '';

    setMsg('pageMsg', '');
  } catch (e) {
    setMsg('pageMsg', getFriendlyMessage(e), true);
    console.error(e);
    return;
  }

  const btn = $('btnSave');
  if (btn) btn.addEventListener('click', () => updateItem(id));
}

function buildBodyFromForm() {
  return {
    Date: $('Date').value || null,
    ExpenseTypeId: Number($('ExpenseTypeID').value || 0),
    Amount: Number($('Amount').value || 0),
    MoneyAccountID: Number($('MoneyAccountID').value || 0),
    Notes: $('Notes').value?.trim() || null,
  };
}

async function updateItem(id) {
  setMsg('pageMsg', 'Saving...', false);
  try {
    const body = buildBodyFromForm();
    await apiSendJson(`${API}/${encodeURIComponent(id)}`, 'PATCH', body);
    window.location.href = 'index.html';
  } catch (e) {
    setMsg('pageMsg', getFriendlyMessage(e), true);
    console.error(e);
  }
}
