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
    if ($('Phone')) $('Phone').value = item.Phone ?? item.phone ?? item.Phone ?? '';
    if ($('Name')) $('Name').value = item.Name ?? item.name ?? item.Name ?? '';

    setMsg('pageMsg', '');
  } catch (e) {
    setMsg('pageMsg', 'Failed to load record from API.', true);
    console.error(e);
    return;
  }

  const btn = $('btnSave');
  if (btn) btn.addEventListener('click', () => updateItem(id));
}

function buildBodyFromForm() {
  const body = {};
  body['Phone'] = $('Phone').value || null;
  body['Name'] = $('Name').value || null;
  return body;
}

async function updateItem(id) {
  setMsg('pageMsg', 'Saving...', false);
  try {
    const body = buildBodyFromForm();
    await apiSendJson(`${API}/${encodeURIComponent(id)}`, 'PATCH', body);
    window.location.href = 'index.html';
  } catch (e) {
    setMsg('pageMsg', 'Update failed. Check API and payload.', true);
    console.error(e);
  }
}
