async function loadBrandDetails() {
    const id = getQueryParam("id");
    if (!id) {
        setMsg("Missing brand id in URL.", true);
        return;
    }

    setMsg("Loading brand...");

    const res = await fetch(`${BRANDS_API}/${encodeURIComponent(id)}`, {
        headers: { Accept: "application/json" }
    });

    if (!res.ok) {
        setMsg(`Failed to load brand (HTTP ${res.status}).`, true);
        return;
    }

    const brand = await res.json();

    // Example: fill UI fields
    $("brandNameText").textContent = brand.name ?? brand.Name ?? "-";
    $("brandDescText").textContent = brand.brandDescription ?? brand.BrandDescription ?? "";
    setMsg("");
}