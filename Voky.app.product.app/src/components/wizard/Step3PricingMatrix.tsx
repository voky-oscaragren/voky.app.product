import { useEffect, useState } from 'react';
import SearchDropdown from '../ui/SearchDropdown';
import AutoFilledBadge from '../ui/AutoFilledBadge';
import { ProductFormData, ProductVariant, MoqPricing } from '../../types/product';
import { suppliers } from '../../types/supplier';

interface Props {
  data: ProductFormData;
  onChange: (field: keyof ProductFormData, value: ProductFormData[keyof ProductFormData]) => void;
  variants: ProductVariant[];
  onVariantsChange: (variants: ProductVariant[]) => void;
}

const currencyOptions = [
  { value: 'SEK', label: 'SEK' },
  { value: 'EUR', label: 'EUR' },
  { value: 'USD', label: 'USD' },
  { value: 'GBP', label: 'GBP' },
];

const inputClass = 'bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm focus:outline-none focus:border-green-500 transition-colors w-full';
const suggestedInputClass = 'bg-wizard-input border border-green-500/40 rounded-lg px-3 py-2.5 text-white text-sm focus:outline-none focus:border-green-500 transition-colors placeholder:text-green-600/60 w-full';

function emptyRows(count: number): MoqPricing[] {
  return Array.from({ length: count }, () => ({ moq: 0, freight: 0, endCustPrice: 0, supplierNetPrice: 0 }));
}

function isDefaultPricing(v: ProductVariant): boolean {
  return v.moqPricing.every(r => r.moq === 0 && r.freight === 0 && r.endCustPrice === 0 && r.supplierNetPrice === 0);
}

export default function Step3PricingMatrix({ data, onChange, variants, onVariantsChange }: Props) {
  const [selectedIndex, setSelectedIndex] = useState(0);

  useEffect(() => {
    if (!data.supplierCurrency && data.headSupplier) {
      const supplier = suppliers.find(s => s.supplierNr === data.headSupplier);
      if (supplier) onChange('supplierCurrency', supplier.supplierCurrency);
    }

    const count = parseInt(data.antalStaflingar, 10);
    if (count > 0 && variants.some(v => v.moqPricing.length === 0)) {
      onVariantsChange(variants.map(v =>
        v.moqPricing.length === 0 ? { ...v, moqPricing: emptyRows(count) } : v
      ));
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const updateRow = (rowIndex: number, field: keyof MoqPricing, value: number) => {
    onVariantsChange(variants.map((v, vi) =>
      vi !== selectedIndex ? v : {
        ...v,
        moqPricing: v.moqPricing.map((row, ri) =>
          ri === rowIndex ? { ...row, [field]: value } : row
        ),
      }
    ));
  };

  const autofillRemaining = () => {
    const source = variants[selectedIndex].moqPricing;
    onVariantsChange(variants.map((v, i) =>
      i !== selectedIndex && isDefaultPricing(v)
        ? { ...v, moqPricing: source.map(r => ({ ...r })) }
        : v
    ));
  };

  const applyToAll = () => {
    const source = variants[selectedIndex].moqPricing;
    onVariantsChange(variants.map((v, i) =>
      i === selectedIndex ? v : { ...v, moqPricing: source.map(r => ({ ...r })) }
    ));
  };

  const selectedVariant = variants[selectedIndex];
  const rows = selectedVariant?.moqPricing ?? [];
  const hasFilledData = rows.some(r => r.moq !== 0 || r.freight !== 0 || r.endCustPrice !== 0 || r.supplierNetPrice !== 0);
  const hasRemainingDefault = variants.some((v, i) => i !== selectedIndex && isDefaultPricing(v));

  return (
    <div className="flex flex-col gap-6">
      <div className="w-1/2 pr-2.5">
        <SearchDropdown
          label="Currency end price"
          value={data.currencyEndPrice}
          onChange={(v) => onChange('currencyEndPrice', v)}
          placeholder="Search currency..."
          options={currencyOptions}
        />
      </div>

      {/* Variant selector */}
      <div>
        <p className="text-wizard-muted text-xs uppercase tracking-wider mb-2">Variant</p>
        <div className="flex flex-wrap gap-2">
          {variants.map((v, i) => (
            <button
              key={i}
              type="button"
              onClick={() => setSelectedIndex(i)}
              className={`px-3 py-1.5 rounded-lg text-sm font-medium transition-colors ${
                selectedIndex === i
                  ? 'bg-green-500 text-black'
                  : 'bg-wizard-input border border-wizard-border text-wizard-muted hover:text-white'
              }`}
            >
              {v.name || v.productNumber}
            </button>
          ))}
        </div>
      </div>

      {/* MOQ Pricing */}
      <div>
        <div className="flex justify-between items-center mb-4">
          <h3 className="text-white font-bold text-base">MOQ Pricing</h3>
          {hasFilledData && (
            <div className="flex gap-2">
              {hasRemainingDefault && (
                <button
                  type="button"
                  onClick={autofillRemaining}
                  className="border border-wizard-border text-wizard-muted hover:text-white hover:border-white px-3 py-1.5 rounded-lg transition-colors text-xs font-medium"
                >
                  Autofill remaining variants
                </button>
              )}
              <button
                type="button"
                onClick={applyToAll}
                className="border border-wizard-border text-wizard-muted hover:text-white hover:border-white px-3 py-1.5 rounded-lg transition-colors text-xs font-medium"
              >
                Apply to all variants
              </button>
            </div>
          )}
        </div>

        {rows.length > 0 ? (
          <div className="flex flex-col gap-3">
            <div className="grid grid-cols-4 gap-3">
              <span className="text-wizard-muted text-xs font-medium uppercase tracking-wider">MOQ</span>
              <span className="text-wizard-muted text-xs font-medium uppercase tracking-wider">Freight</span>
              <span className="text-wizard-muted text-xs font-medium uppercase tracking-wider flex items-center gap-1.5">
                End. Cust. Price <AutoFilledBadge />
              </span>
              <span className="text-wizard-muted text-xs font-medium uppercase tracking-wider">Supplier Net Price</span>
            </div>
            {rows.map((row, i) => (
              <div key={i} className="grid grid-cols-4 gap-3 items-center">
                <input type="number" value={row.moq} onChange={(e) => updateRow(i, 'moq', Number(e.target.value))} className={inputClass} />
                <input type="number" value={row.freight} onChange={(e) => updateRow(i, 'freight', Number(e.target.value))} className={inputClass} />
                <input type="number" value={row.endCustPrice || ''} onChange={(e) => updateRow(i, 'endCustPrice', Number(e.target.value))} placeholder="Suggested price" className={suggestedInputClass} />
                <input type="number" value={row.supplierNetPrice} onChange={(e) => updateRow(i, 'supplierNetPrice', Number(e.target.value))} className={inputClass} />
              </div>
            ))}
          </div>
        ) : (
          <p className="text-wizard-muted text-sm">
            {parseInt(data.antalStaflingar, 10) > 0
              ? 'Loading pricing rows…'
              : 'Set "Antal stafflingar" in Step 1 to generate MOQ rows.'}
          </p>
        )}
      </div>
    </div>
  );
}
