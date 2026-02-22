const API = "https://localhost:7209/api/MoneyTransactions";

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
