// API base for this module
const API = "https://localhost:7209/api/Persons";

// Wire search input to reload list (same pattern as Brands)
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
});
