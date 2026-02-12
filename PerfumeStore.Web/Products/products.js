// 4) Load brands from the API and render them into the table
async function loadBrands(searchText = "") {
    const tbody = $("productsRows");
    const emptyState = $("emptyState");
    const tableCard = $("tableCard");
    const countChip = $("productsCount");

    if (!tbody) return;

    setMsg("pageMsg", "Loading products...");

    const url =
        searchText && searchText.trim().length > 0
            ? `${PRODUCTS_API}?search=${encodeURIComponent(searchText.trim())}`
            : PRODUCTS_API;

    const res = await fetch(url, { headers: { Accept: "application/json" } });

    if (!res.ok) {
        setMsg("pageMsg", "Failed to load products from API.", true);
        // Keep UI safe: show empty state and hide table if something goes wrong
        tbody.innerHTML = "";
        if (countChip) countChip.textContent = "0";
        if (emptyState) emptyState.style.display = "";
        if (tableCard) tableCard.style.display = "none";
        return;
    }

    const brands = await res.json();
    const list = Array.isArray(brands) ? brands : [];

    // Update count
    if (countChip) countChip.textContent = String(list.length);

    // Clear rows
    tbody.innerHTML = "";

    // Empty handling (show empty-state, hide table)
    if (list.length === 0) {
        setMsg("pageMsg", ""); // cleaner look; empty-state already explains
        if (emptyState) emptyState.style.display = "";
        if (tableCard) tableCard.style.display = "none";
        return;
    }

    // Has data (hide empty-state, show table)
    if (emptyState) emptyState.style.display = "none";
    if (tableCard) tableCard.style.display = "";

    setMsg("pageMsg", `Loaded ${list.length} brand(s).`);

    list.forEach((b, index) => {
        const id = b.id ?? b.ID ?? "";
        const name = b.name ?? b.Name ?? "-";
        const desc =
            b.brandDescription ??
            b.BrandDescription ??
            b.description ??
            b.Description ??
            "";

        const tr = document.createElement("tr");
        tr.innerHTML = `
      <td>${index + 1}</td>
      <td><a class="brand-link" href="view.html?id=${encodeURIComponent(
            id
        )}">${escapeHtml(name)}</a></td>
      <td>${escapeHtml(desc)}</td>
      <td class="actions-cell">
        <a class="btn-edit" href="edit.html?id=${encodeURIComponent(
            id
        )}">Edit</a>
      </td>
    `;
        tbody.appendChild(tr);
    });
}