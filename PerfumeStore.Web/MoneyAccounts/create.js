function buildBodyFromForm() {
  const body = {};
  body['AccountName'] = $('AccountName').value || null;
  body['CurrentBalance'] = Number($('CurrentBalance').value || 0);
  body['Notes'] = $('Notes').value || null;
  return body;
}

async function createItem() {
  setMsg('pageMsg', 'Saving...', false);
  try {
    const body = buildBodyFromForm();
    await apiSendJson(API, 'POST', body);
    window.location.href = 'index.html';
  } catch (e) {
    setMsg('pageMsg', 'Save failed. Check API and payload.', true);
    console.error(e);
  }
}




