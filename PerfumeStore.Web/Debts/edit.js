async function initPage() {
  try {
    await fillPeopleSelect("PersonId", "https://localhost:7209/api/Persons");
  } catch (e) {
    console.error(e);
  }
}

async function fillPeopleSelect(selectId, apiUrl) {
  const el = $(selectId);
  if (!el) return;

  el.innerHTML = `<option value="">-- Select --</option>`;
  let list = [];
  try {
    list = await apiGetJson(apiUrl);
  } catch {
    list = [];
  }

  (Array.isArray(list) ? list : []).forEach((p) => {
    const id = p.id ?? p.ID ?? p.Id ?? "";
    if (!id) return;

    const name = p.name ?? p.Name ?? "";
    const phone = p.phone ?? p.Phone ?? "";

    const opt = document.createElement("option");
    opt.value = id;
    opt.textContent = name ? `${name}${phone ? ` (${phone})` : ""}` : (phone || id);
    el.appendChild(opt);
  });
}

function wireExclusiveLinkInputs() {
  const sales = $("SalesInvoiceId");
  const purchase = $("PurchaseInvoiceId");
  const person = $("PersonId");

  function apply() {
    const salesVal = sales?.value?.trim() || "";
    const purchaseVal = purchase?.value?.trim() || "";
    const personVal = person?.value?.trim() || "";

    let active = "";
    if (salesVal) active = "sales";
    else if (purchaseVal) active = "purchase";
    else if (personVal) active = "person";

    if (!active) {
      if (sales) sales.disabled = false;
      if (purchase) purchase.disabled = false;
      if (person) person.disabled = false;
      return;
    }

    if (active === "sales") {
      if (purchase) purchase.value = "";
      if (person) person.value = "";
      if (sales) sales.disabled = false;
      if (purchase) purchase.disabled = true;
      if (person) person.disabled = true;
    } else if (active === "purchase") {
      if (sales) sales.value = "";
      if (person) person.value = "";
      if (sales) sales.disabled = true;
      if (purchase) purchase.disabled = false;
      if (person) person.disabled = true;
    } else if (active === "person") {
      if (sales) sales.value = "";
      if (purchase) purchase.value = "";
      if (sales) sales.disabled = true;
      if (purchase) purchase.disabled = true;
      if (person) person.disabled = false;
    }
  }

  if (sales) sales.addEventListener("input", apply);
  if (purchase) purchase.addEventListener("input", apply);
  if (person) person.addEventListener("change", apply);

  apply();
}

function buildBodyFromForm() {
  const amount = Number($("Amount").value || 0);

  const salesRaw = $("SalesInvoiceId")?.value?.trim() || "";
  const purchaseRaw = $("PurchaseInvoiceId")?.value?.trim() || "";
  const personIdRaw = $("PersonId")?.value?.trim() || "";

  const salesId = salesRaw ? Number(salesRaw) : null;
  const purchaseId = purchaseRaw ? Number(purchaseRaw) : null;

  return {
    Amount: amount,
    SalesInvoiceId: Number.isFinite(salesId) ? salesId : null,
    PurchaseInvoiceId: Number.isFinite(purchaseId) ? purchaseId : null,
    PersonId: personIdRaw || null,
    Notes: $("Notes")?.value || null
  };
}

function validateBody(body) {
  if (!body.Amount || body.Amount <= 0) return "Amount is required and must be greater than 0.";

  const linksCount =
    (body.SalesInvoiceId ? 1 : 0) +
    (body.PurchaseInvoiceId ? 1 : 0) +
    (body.PersonId ? 1 : 0);

  if (linksCount === 0)
    return "Please link the debt to exactly one: Sales Invoice ID OR Purchase Invoice ID OR Person.";

  if (linksCount > 1)
    return "Only one link is allowed. Clear the other fields and try again.";

  if ($("SalesInvoiceId").value.trim() && !body.SalesInvoiceId)
    return "Sales Invoice ID must be a valid number.";

  if ($("PurchaseInvoiceId").value.trim() && !body.PurchaseInvoiceId)
    return "Purchase Invoice ID must be a valid number.";

  return null;
}

async function initEdit() {
  const id = getQueryParam("id");
  if (!id) {
    setMsg("pageMsg", "Missing id in URL.", true);
    return;
  }

  // load dropdown first (so we can set selected value)
  if (typeof initPage === "function") await initPage();

  setMsg("pageMsg", "Loading...", false);

  try {
    const item = await apiGetJson(`${API}/${encodeURIComponent(id)}`);

    // Try many casing variants to be safe
    const amount = item.amount ?? item.Amount ?? "";
    const salesId = item.salesInvoiceId ?? item.SalesInvoiceId ?? item.salesinvoiceid ?? "";
    const purchaseId = item.purchaseInvoiceId ?? item.PurchaseInvoiceId ?? item.purchaseinvoiceid ?? "";
    const personId = item.personId ?? item.PersonId ?? item.personid ?? "";
    const notes = item.notes ?? item.Notes ?? "";

    if ($("Amount")) $("Amount").value = amount;
    if ($("SalesInvoiceId")) $("SalesInvoiceId").value = salesId ?? "";
    if ($("PurchaseInvoiceId")) $("PurchaseInvoiceId").value = purchaseId ?? "";
    if ($("PersonId")) $("PersonId").value = personId ?? "";
    if ($("Notes")) $("Notes").value = notes ?? "";

    setMsg("pageMsg", "");
  } catch (e) {
    const validationMsg = getFriendlyMessage(e);

    setMsg(
      'pageMsg',
      validationMsg ?? "Failed to load record from API.",
      true
    );
    console.error(e);
    return;
  }

  // enforce one-link behavior after values are set
  wireExclusiveLinkInputs();

  const btn = $("btnSave");
  if (btn) btn.addEventListener("click", () => updateItem(id));
}

async function updateItem(id) {
  setMsg("pageMsg", "Saving...", false);
  try {
    const body = buildBodyFromForm();
    const err = validateBody(body);
    if (err) {
      setMsg("pageMsg", err, true);
      return;
    }

    await apiSendJson(`${API}/${encodeURIComponent(id)}`, "PATCH", body);
    window.location.href = "index.html";
  } catch (e) {
    const validationMsg = getFriendlyMessage(e);

    setMsg(
      'pageMsg',
      validationMsg ?? "Update failed. Check API and payload.",
      true
    );
    console.error(e);
  }
}