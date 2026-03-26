async function loadView() {
  const id = getQueryParam('id');
  if (!id) {
    setMsg('pageMsg', 'Missing id in URL.', true);
    return;
  }

  setMsg('pageMsg', 'Loading...', false);

  try {
    const item = await apiGetJson(`${API}/${encodeURIComponent(id)}`);
    const lines = [];
    for (const [k, v] of Object.entries(item || {})) {
      if (typeof v === 'object' && v !== null) continue;
      lines.push(`<div style="color:rgba(232,238,252,.75);font-weight:800;">${escapeHtml(k)}</div><div>${escapeHtml(v)}</div>`);
    }
    $('details').innerHTML = lines.join('');
    setMsg('pageMsg', '');
  } catch (e) {
    setMsg('pageMsg', 'Failed to load record from API.', true);
    console.error(e);
  }
}
