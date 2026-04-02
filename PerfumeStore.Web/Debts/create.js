function buildBodyFromForm() {
  const amount = Number($("Amount").value || 0);

  const salesInvoiceIdRaw = $("SalesInvoiceId").value?.trim() || "";
  const purchaseInvoiceIdRaw = $("PurchaseInvoiceId").value?.trim() || "";
  const personIdRaw = $("PersonId").value?.trim() || "";

  const salesInvoiceId = salesInvoiceIdRaw ? Number(salesInvoiceIdRaw) : null;
  const purchaseInvoiceId = purchaseInvoiceIdRaw ? Number(purchaseInvoiceIdRaw) : null;

  return {
    Amount: amount,
    SalesInvoiceId: Number.isFinite(salesInvoiceId) ? salesInvoiceId : null,
    PurchaseInvoiceId: Number.isFinite(purchaseInvoiceId) ? purchaseInvoiceId : null,
    PersonId: personIdRaw || null,
    Notes: $("Notes").value || null
  };
}

function validateBody(body) {
  if (!body.Amount || body.Amount <= 0) return "Amount is required and must be greater than 0.";

  const linksCount =
    (body.SalesInvoiceId ? 1 : 0) +
    (body.PurchaseInvoiceId ? 1 : 0) +
    (body.PersonId ? 1 : 0);

  if (linksCount === 0)
    return "Please link the debt to exactly one: Sales Invoice ID OR Purchase Invoice ID OR Person / Supplier.";

  if (linksCount > 1)
    return "Only one link is allowed. Clear the other fields and try again.";

  if ($("SalesInvoiceId").value.trim() && !body.SalesInvoiceId)
    return "Sales Invoice ID must be a valid number.";

  if ($("PurchaseInvoiceId").value.trim() && !body.PurchaseInvoiceId)
    return "Purchase Invoice ID must be a valid number.";

  return null;
}

async function createItem() {
  setMsg("pageMsg", "Saving...", false);

  try {
    const body = buildBodyFromForm();
    const err = validateBody(body);
    if (err) {
      setMsg("pageMsg", err, true);
      return;
    }

    await apiSendJson(API, "POST", body);
    window.location.href = "index.html";
  } catch (e) {
    const validationMsg = getFriendlyMessage(e);

    setMsg(
      'pageMsg',
      validationMsg ?? 'Save failed. Check API and payload.',
      true
    );
    console.error(e);
  }
}

async function initPage() {
  try {
    await fillDebtPartiesSelect("PersonId");
  } catch (e) {
    console.error(e);
  }

  wireExclusiveLinkInputs();
}

function wireExclusiveLinkInputs() {
  const sales = $("SalesInvoiceId");
  const purchase = $("PurchaseInvoiceId");
  const person = $("PersonId");

  function apply() {
    const salesVal = sales.value.trim();
    const purchaseVal = purchase.value.trim();
    const personVal = person.value.trim();

    let active = "";
    if (salesVal) active = "sales";
    else if (purchaseVal) active = "purchase";
    else if (personVal) active = "person";

    if (!active) {
      sales.disabled = false;
      purchase.disabled = false;
      person.disabled = false;
      return;
    }

    if (active === "sales") {
      purchase.value = "";
      person.value = "";
      sales.disabled = false;
      purchase.disabled = true;
      person.disabled = true;
    } else if (active === "purchase") {
      sales.value = "";
      person.value = "";
      sales.disabled = true;
      purchase.disabled = false;
      person.disabled = true;
    } else if (active === "person") {
      sales.value = "";
      purchase.value = "";
      sales.disabled = true;
      purchase.disabled = true;
      person.disabled = false;
    }
  }

  sales.addEventListener("input", apply);
  purchase.addEventListener("input", apply);
  person.addEventListener("change", apply);

  apply();
}

async function fillDebtPartiesSelect(selectId) {
  const el = $(selectId);
  if (!el) return;

  el.innerHTML = `<option value="">-- Select Person / Supplier --</option>`;

  let persons = [];
  let suppliers = [];

  try { persons = await apiGetJson("https://localhost:7209/api/Persons"); } catch { persons = []; }
  try { suppliers = await apiGetJson("https://localhost:7209/api/Suppliers"); } catch { suppliers = []; }

  const seen = new Set();
  const items = [];

  (Array.isArray(persons) ? persons : []).forEach((p) => items.push({ ...p, _partyType: "Person" }));
  (Array.isArray(suppliers) ? suppliers : []).forEach((s) => items.push({ ...s, _partyType: "Supplier" }));

  items.forEach((p) => {
    const id = p.id ?? p.ID ?? p.Id ?? "";
    if (!id || seen.has(id)) return;
    seen.add(id);

    const name = p.name ?? p.Name ?? "";
    const phone = p.phone ?? p.Phone ?? "";
    const type = p._partyType || "Person";

    const opt = document.createElement("option");
    opt.value = id;
    opt.textContent = `${type} - ${name ? `${name}${phone ? ` (${phone})` : ""}` : (phone || id)}`;
    el.appendChild(opt);
  });
}
