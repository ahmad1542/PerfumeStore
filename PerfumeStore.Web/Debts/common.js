const API = window.API_ENDPOINTS.debts;

document.addEventListener("DOMContentLoaded", () => {
  const searchBox = $("searchInput");
  if (!searchBox) return;

  let timer = null;
  searchBox.addEventListener("input", () => {
    clearTimeout(timer);
    timer = setTimeout(() => {
      if (typeof loadList === "function") loadList(searchBox.value);
    }, 350);
  });
  $("ShowSettled")?.addEventListener("change", () => {
    loadList($("searchInput")?.value || "");
  });
});
