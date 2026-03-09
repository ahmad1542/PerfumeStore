const API = "https://localhost:7209/api/SalesInvoices";

document.addEventListener("DOMContentLoaded", () => {
  const searchBox = $("searchInput");
  const fromDateInput = $("fromDateInput");
  const toDateInput = $("toDateInput");

  let timer = null;

  const triggerLoad = () => {
    clearTimeout(timer);
    timer = setTimeout(() => {
      if (typeof loadList === "function") {
        loadList(searchBox?.value || "");
      }
    }, 350);
  };

  if (searchBox) {
    searchBox.addEventListener("input", triggerLoad);
  }

  if (fromDateInput) {
    fromDateInput.addEventListener("change", triggerLoad);
  }

  if (toDateInput) {
    toDateInput.addEventListener("change", triggerLoad);
  }
});