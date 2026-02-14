async function loadProducts(searchText = "") {
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

    let res;

    try {
        res = await fetch(url, { headers: { Accept: "application/json" } });
    } catch {
        setMsg("pageMsg", "Failed to load products.", true);
        tbody.innerHTML = "";
        if (countChip) countChip.textContent = "0";
        if (emptyState) emptyState.style.display = "";
        if (tableCard) tableCard.style.display = "none";
        return;
    }

    if (!res.ok) {
        setMsg("pageMsg", "Failed to load products.", true);
        tbody.innerHTML = "";
        if (countChip) countChip.textContent = "0";
        if (emptyState) emptyState.style.display = "";
        if (tableCard) tableCard.style.display = "none";
        return;
    }

    const data = await res.json();
    const list = Array.isArray(data) ? data : [];

    if (countChip) countChip.textContent = String(list.length);
    tbody.innerHTML = "";

    if (list.length === 0) {
        setMsg("pageMsg", "");
        if (emptyState) emptyState.style.display = "";
        if (tableCard) tableCard.style.display = "none";
        return;
    }

    if (emptyState) emptyState.style.display = "none";
    if (tableCard) tableCard.style.display = "";

    setMsg("pageMsg", `Loaded ${list.length} product(s).`);

    list.forEach((p, index) => {
        const id = p.id ?? p.ID ?? "";
        const name = p.name ?? p.Name ?? "-";
        const capacity = p.capacity ?? p.Capacity ?? "-";
        const salePrice = p.salePrice ?? p.SalePrice ?? "-";
        const costPrice = p.costPrice ?? p.CostPrice ?? "-";

        const category =
            p.categoryName ??
            p.CategoryName ??
            p.category?.name ??
            p.category?.Name ??
            "-";

        const brand =
            p.brandName ??
            p.BrandName ??
            p.brand?.name ??
            p.brand?.Name ??
            "-";

        const tr = document.createElement("tr");
        tr.innerHTML = `
      <td>${index + 1}</td>
      <td>${escapeHtml(name)}</td>
      <td>${escapeHtml(String(capacity))}</td>
      <td>${escapeHtml(category)}</td>
      <td>${escapeHtml(brand)}</td>
      <td>${escapeHtml(String(salePrice))}</td>
      <td>${escapeHtml(String(costPrice))}</td>
      <td class="actions-cell">
        <a class="btn-edit" href="edit.html?id=${encodeURIComponent(id)}">Edit</a>
      </td>
    `;

        tbody.appendChild(tr);
    });
}
