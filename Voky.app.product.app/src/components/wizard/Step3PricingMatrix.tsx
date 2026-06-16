import SearchDropdown from '../ui/SearchDropdown';
import AutoFilledBadge from '../ui/AutoFilledBadge';
import { ProductFormData, MoqPricing } from '../../types/product';

interface Props {
  data: ProductFormData;
  onChange: (field: keyof ProductFormData, value: ProductFormData[keyof ProductFormData]) => void;
}

export default function Step3PricingMatrix({ data, onChange }: Props) {
  const addMoq = () => {
    onChange('moqPricing', [...data.moqPricing, { moq: 0, endCustPrice: 0, supplierNetPrice: 0 }]);
  };

  const removeMoq = (index: number) => {
    onChange('moqPricing', data.moqPricing.filter((_, i) => i !== index));
  };

  const updateMoq = (index: number, field: keyof MoqPricing, value: number) => {
    const updated = data.moqPricing.map((row, i) =>
      i === index ? { ...row, [field]: value } : row
    );
    onChange('moqPricing', updated);
  };

  return (
    <div className="flex flex-col gap-6">
      {/* Auto-filled summary */}
      <div className="flex flex-wrap gap-4">
        <div className="flex flex-col">
          <span className="text-wizard-muted text-xs mb-1">Product number</span>
          <div className="flex items-center">
            <span className="bg-wizard-input border border-wizard-border rounded px-3 py-1.5 text-white text-sm">
              {data.productNumber || 'PROD-84920-X'}
            </span>
            <AutoFilledBadge />
          </div>
        </div>
        <div className="flex flex-col">
          <span className="text-wizard-muted text-xs mb-1">Name</span>
          <div className="flex items-center">
            <span className="bg-wizard-input border border-wizard-border rounded px-3 py-1.5 text-white text-sm">
              {data.name || 'Ergonomic Office Chair v4'}
            </span>
            <AutoFilledBadge />
          </div>
        </div>
        <div className="flex flex-col">
          <span className="text-wizard-muted text-xs mb-1">Art. nr Varianthead</span>
          <div className="flex items-center">
            <span className="bg-wizard-input border border-wizard-border rounded px-3 py-1.5 text-white text-sm">
              {data.artNrVarianthead || 'VH-84920-BASE-BLK'}
            </span>
            <AutoFilledBadge />
          </div>
        </div>
      </div>

      <div className="border-t border-wizard-border pt-5 grid grid-cols-2 gap-5">
        <SearchDropdown
          label="Currency end price"
          value={data.currencyEndPrice}
          onChange={(v) => onChange('currencyEndPrice', v)}
          placeholder="Search currency..."
        />
        <SearchDropdown
          label="Supplier currency"
          value={data.supplierCurrency}
          onChange={(v) => onChange('supplierCurrency', v)}
          placeholder="Search currency..."
        />
      </div>

      {/* MOQ Pricing */}
      <div>
        <div className="flex justify-between items-center mb-4">
          <h3 className="text-white font-bold text-base">MOQ Pricing</h3>
          <button
            type="button"
            onClick={addMoq}
            className="border border-green-500 text-green-400 hover:bg-green-500/10 px-4 py-2 rounded-lg transition-colors text-sm font-medium"
          >
            + Add MOQ
          </button>
        </div>

        {data.moqPricing.length > 0 && (
          <div className="flex flex-col gap-3">
            {/* Column headers */}
            <div className="grid grid-cols-[1fr_1fr_1fr_40px] gap-3">
              <span className="text-wizard-muted text-xs font-medium">MOQ</span>
              <span className="text-wizard-muted text-xs font-medium">End. Cust. Price</span>
              <span className="text-wizard-muted text-xs font-medium">Supplier Net Price</span>
              <span />
            </div>
            {data.moqPricing.map((row, i) => (
              <div key={i} className="grid grid-cols-[1fr_1fr_1fr_40px] gap-3 items-center">
                <input
                  type="number"
                  value={row.moq}
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => updateMoq(i, 'moq', Number(e.target.value))}
                  className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm focus:outline-none focus:border-green-500 transition-colors"
                />
                <input
                  type="number"
                  value={row.endCustPrice}
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => updateMoq(i, 'endCustPrice', Number(e.target.value))}
                  className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm focus:outline-none focus:border-green-500 transition-colors"
                />
                <input
                  type="number"
                  value={row.supplierNetPrice}
                  onChange={(e: React.ChangeEvent<HTMLInputElement>) => updateMoq(i, 'supplierNetPrice', Number(e.target.value))}
                  className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm focus:outline-none focus:border-green-500 transition-colors"
                />
                <button
                  type="button"
                  onClick={() => removeMoq(i)}
                  className="text-wizard-muted hover:text-white transition-colors text-lg leading-none flex items-center justify-center"
                >
                  ×
                </button>
              </div>
            ))}
          </div>
        )}

        {data.moqPricing.length === 0 && (
          <p className="text-wizard-muted text-sm">No MOQ pricing added. Click "+ Add MOQ" to add a row.</p>
        )}
      </div>
    </div>
  );
}
