import { ProductFormData } from '../../types/product';

interface Props {
  data: ProductFormData;
}

function KVRow({ label, value }: { label: string; value: string }) {
  return (
    <div className="flex py-2">
      <span className="text-wizard-muted text-sm w-48 shrink-0">{label}</span>
      <span className="text-white text-sm">{value || '—'}</span>
    </div>
  );
}

function Chip({ label }: { label: string }) {
  return (
    <span className="bg-wizard-badge text-white text-sm px-3 py-1 rounded-full">
      {label}
    </span>
  );
}

export default function Step5Overview({ data }: Props) {
  return (
    <div className="flex flex-col">
      {/* Product Details */}
      <section>
        <h2 className="text-xl font-bold text-white mb-2">Product Details</h2>
        <div className="divide-y divide-wizard-border">
          <KVRow label="Product number" value={data.productNumber} />
          <KVRow label="Name" value={data.name} />
          <KVRow label="Supplier art. nr / name" value={data.supplierArtNr} />
          <KVRow label="Art. nr Varianthead" value={data.artNrVarianthead} />
          <KVRow label="Head supplier" value={data.headSupplier} />
          <KVRow label="Art. nr start cost" value={data.artNrStartCost} />
          <KVRow label="Amount start cost" value={data.amountStartCost} />
          <KVRow label="Lifecycle" value={data.lifecycle} />
          <KVRow label="Product description" value={data.productDescription} />
          <KVRow label="MOQ Customer" value={data.moqCustomer} />
          <KVRow label="Send to opti" value={data.sendToOpti ? 'Yes' : 'No'} />
        </div>
      </section>

      {/* Questions */}
      <section className="border-t border-wizard-border pt-6 mt-6">
        <h2 className="text-xl font-bold text-white mb-4">Questions</h2>
        {data.questions.length === 0 ? (
          <p className="text-wizard-muted text-sm">No questions added.</p>
        ) : (
          <div className="flex flex-wrap gap-2">
            {data.questions.map((q, i) => (
              <Chip key={i} label={q} />
            ))}
          </div>
        )}
      </section>

      {/* Pricing */}
      <section className="border-t border-wizard-border pt-6 mt-6">
        <h2 className="text-xl font-bold text-white mb-4">Pricing</h2>
        <div className="divide-y divide-wizard-border mb-4">
          <KVRow label="Currency end price" value={data.currencyEndPrice} />
          <KVRow label="Supplier currency" value={data.supplierCurrency} />
        </div>
        {data.moqPricing.length > 0 && (
          <table className="w-full text-sm">
            <thead>
              <tr className="text-wizard-muted uppercase text-xs">
                <th className="text-left pb-2 font-medium">MOQ</th>
                <th className="text-left pb-2 font-medium">End Price</th>
                <th className="text-left pb-2 font-medium">Net Price</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-wizard-border">
              {data.moqPricing.map((row, i) => (
                <tr key={i}>
                  <td className="py-2 text-white">{row.moq}</td>
                  <td className="py-2 text-white">{row.endCustPrice.toFixed(2)}</td>
                  <td className="py-2 text-white">{row.supplierNetPrice.toFixed(2)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
        {data.moqPricing.length === 0 && (
          <p className="text-wizard-muted text-sm">No MOQ pricing configured.</p>
        )}
      </section>

      {/* Specifications */}
      <section className="border-t border-wizard-border pt-6 mt-6">
        <h2 className="text-xl font-bold text-white mb-2">Specifications</h2>
        <div className="divide-y divide-wizard-border">
          <KVRow label="Brand" value={data.brand} />
          <KVRow label="Delivery information" value={data.deliveryInformation} />
          <KVRow label="Deliv. time min" value={data.delivTimeMin ? `${data.delivTimeMin} working days` : ''} />
          <KVRow label="Deliv. time max" value={data.delivTimeMax ? `${data.delivTimeMax} working days` : ''} />
          <KVRow label="Size info" value={data.sizeInfo} />
          <KVRow label="Material info" value={data.materialInfo} />
        </div>
        <div className="flex gap-8 mt-4">
          <div>
            <span className="text-wizard-muted text-xs uppercase tracking-wider block mb-2">Tags</span>
            <div className="flex flex-wrap gap-2">
              {data.tags.length === 0 ? (
                <span className="text-wizard-muted text-sm">None</span>
              ) : (
                data.tags.map((t) => <Chip key={t} label={t} />)
              )}
            </div>
          </div>
          <div>
            <span className="text-wizard-muted text-xs uppercase tracking-wider block mb-2">Categories</span>
            <div className="flex flex-wrap gap-2">
              {data.categories.length === 0 ? (
                <span className="text-wizard-muted text-sm">None</span>
              ) : (
                data.categories.map((c) => <Chip key={c} label={c} />)
              )}
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}
