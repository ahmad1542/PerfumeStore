function buildBodyFromForm() {
  const body = {};
  body['Phone'] = $('Phone').value || null;
  body['Name'] = $('Name').value || null;
  return body;
}

async function createItem() {
  setMsg('pageMsg', 'Saving...', false);
  try {
    const body = buildBodyFromForm();
    await apiSendJson(API, 'POST', body);
    setMsg('pageMsg', 'Saved successfully.', false);
  } catch (e) {
    setMsg('pageMsg', 'Save failed. Check API and payload.', true);
    console.error(e);
  }
}