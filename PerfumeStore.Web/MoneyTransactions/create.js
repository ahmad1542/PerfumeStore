function buildBodyFromForm() {
  const fromId = $('FromMoneyAccountID').value;
  const toId = $('ToMoneyAccountID').value;

  return {
    Date: $('Date').value || null, // yyyy-MM-dd
    FromMoneyAccountID: fromId ? Number(fromId) : null,
    ToMoneyAccountID: toId ? Number(toId) : null,
    TransferAmount: Number($('TransferAmount').value || 0),
    Notes: $('Notes').value || null
  };
}

async function createItem() {
  setMsg('pageMsg', 'Saving...', false);

  try {
    const body = buildBodyFromForm();

    if (!body.FromMoneyAccountID || !body.ToMoneyAccountID) {
      setMsg('pageMsg', 'Please select From and To accounts.', true);
      return;
    }
    if (body.FromMoneyAccountID === body.ToMoneyAccountID) {
      setMsg('pageMsg', 'From and To accounts cannot be the same.', true);
      return;
    }
    if (body.TransferAmount <= 0) {
      setMsg('pageMsg', 'Transfer amount must be greater than 0.', true);
      return;
    }
    const fromBal = getSelectedBalance('FromMoneyAccountID');
    if (fromBal != null && body.TransferAmount > fromBal) {
      setMsg('pageMsg', `Insufficient balance. Available: ${fromBal}`, true);
      return;
    }

    await apiSendJson(API, 'POST', body);
    window.location.href = 'index.html';
  } catch (e) {
    const validationMsg = getFriendlyMessage(e);

    setMsg(
      'pageMsg',
      validationMsg ?? 'Save failed. Check API and payload.',
      true
    );
    console.error(e);
  }
}

async function initPage() {
  try {
    await fillMoneyAccounts('FromMoneyAccountID');
    await fillMoneyAccounts('ToMoneyAccountID');
  } catch (e) { console.error(e); }
}

async function fillMoneyAccounts(selectId) {
  const el = $(selectId);
  if (!el) return;
  el.innerHTML = `<option value="">-- Select --</option>`;
  let list = [];
  try { list = await apiGetJson("https://localhost:7209/api/MoneyAccounts"); } catch { list = []; }
  (Array.isArray(list) ? list : []).forEach(a => {
    const id = a.id ?? a.ID ?? a.Id ?? "";
    const name = a.accountName ?? a.AccountName ?? ("Account #" + id);
    const bal = a.currentBalance ?? a.CurrentBalance ?? null;
    if (id === "") return;

    const opt = document.createElement("option");
    opt.value = id;

    opt.dataset.balance = String(bal);

    opt.textContent = bal != null
      ? `${name} (Balance: ${bal})`
      : name;
    el.appendChild(opt);
  });
}
