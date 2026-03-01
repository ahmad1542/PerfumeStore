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

function getSelectedBalance(selectId) {
  const el = $(selectId);
  if (!el) return null;

  const opt = el.options[el.selectedIndex];
  if (!opt) return null;

  const balStr = opt.dataset.balance;
  if (balStr == null || balStr === "") return null;

  const bal = Number(balStr);
  return Number.isFinite(bal) ? bal : null;
}