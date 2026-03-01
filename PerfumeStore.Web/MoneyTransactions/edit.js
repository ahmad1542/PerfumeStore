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

async function initEdit() {
  const id = getQueryParam('id');
  if (!id) {
    setMsg('pageMsg', 'Missing id in URL.', true);
    return;
  }

  if (typeof initPage === 'function') await initPage();

  setMsg('pageMsg', 'Loading...', false);

  try {
    const item = await apiGetJson(`${API}/${encodeURIComponent(id)}`);
    if ($('Date')) $('Date').value = item.Date ?? item.date ?? item.Date ?? '';
    if ($('FromMoneyAccountID')) $('FromMoneyAccountID').value = item.FromMoneyAccountID ?? item.frommoneyaccountid ?? item.Frommoneyaccountid ?? '';
    if ($('ToMoneyAccountID')) $('ToMoneyAccountID').value = item.ToMoneyAccountID ?? item.tomoneyaccountid ?? item.Tomoneyaccountid ?? '';
    if ($('TransferAmount')) $('TransferAmount').value = item.TransferAmount ?? item.transferamount ?? item.Transferamount ?? '';
    if ($('Notes')) $('Notes').value = item.Notes ?? item.notes ?? item.Notes ?? '';

    setMsg('pageMsg', '');
  } catch (e) {
    const validationMsg = getFriendlyMessage(e);

    setMsg(
      'pageMsg',
      validationMsg ?? 'Failed to load record from API.',
      true
    );
    console.error(e);
    return;
  }

  const btn = $('btnSave');
  if (btn) btn.addEventListener('click', () => updateItem(id));
}

function buildBodyFromForm() {
  const body = {};
  body['Date'] = $('Date').value || null;
  body['FromMoneyAccountID'] = $('FromMoneyAccountID').value || null;
  body['ToMoneyAccountID'] = $('ToMoneyAccountID').value || null;
  body['TransferAmount'] = Number($('TransferAmount').value || 0);
  body['Notes'] = $('Notes').value || null;
  return body;
}

async function updateItem(id) {
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

    await apiSendJson(`${API}/${encodeURIComponent(id)}`, 'PATCH', body);
    window.location.href = 'index.html';
  } catch (e) {
    const validationMsg = getFriendlyMessage(e);

    setMsg(
      'pageMsg',
      validationMsg ?? 'Update failed. Please check your input and try again.',
      true
    );
    console.error(e);
  }
}
