import { useEffect, useState } from 'react';
import SearchDropdown from '../ui/SearchDropdown';
import AutoFilledBadge from '../ui/AutoFilledBadge';
import FormInput from '../ui/FormInput';
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
  return Array.from({ length: count }, () => ({ moq: 0, freight: 0, freightType: '', endCustPrice: 0, supplierNetPrice: 0 }));
}

function withMinMoq(v: ProductVariant): ProductVariant {
  const nonZero = v.moqPricing.map(r => r.moq).filter(m => m > 0);
  return { ...v, moqCustomer: nonZero.length > 0 ? String(Math.min(...nonZero)) : '' };
}

export default function Step3PricingMatrix({ data, onChange, variants, onVariantsChange }: Props) {
  const [selectedIndex, setSelectedIndex] = useState(0);
  const [syncAll, setSyncAll] = useState(false);
  const [showAllVariants, setShowAllVariants] = useState(false);

  useEffect(() => {
    if (!data.currencyEndPrice) onChange('currencyEndPrice', 'SEK');

    if (!data.supplierCurrency && data.headSupplier) {
      const supplier = suppliers.find(s => s.supplierNr === data.headSupplier);
      if (supplier) onChange('supplierCurrency', supplier.supplierCurrency);
    }

    if (variants.some(v => v.moqPricing.length === 0)) {
      onVariantsChange(variants.map(v => {
        if (v.moqPricing.length > 0) return v;
        const count = parseInt(v.antalStaflingar, 10) || parseInt(data.antalStaflingar, 10);
        return count > 0 ? withMinMoq({ ...v, moqPricing: emptyRows(count) }) : v;
      }));
    }
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const addRow = () => {
    const newRow = { moq: 0, freight: 0, freightType: '', endCustPrice: 0, supplierNetPrice: 0 };
    onVariantsChange(variants.map((v, vi) => {
      if (!syncAll && vi !== selectedIndex) return v;
      return withMinMoq({ ...v, moqPricing: [...v.moqPricing, { ...newRow }] });
    }));
  };

  const removeRow = (rowIndex: number) => {
    onVariantsChange(variants.map((v, vi) => {
      if (!syncAll && vi !== selectedIndex) return v;
      return withMinMoq({ ...v, moqPricing: v.moqPricing.filter((_, ri) => ri !== rowIndex) });
    }));
  };

  const updateRow = (rowIndex: number, field: keyof MoqPricing, value: number | string) => {
    onVariantsChange(variants.map((v, vi) => {
      if (!syncAll && vi !== selectedIndex) return v;
      return withMinMoq({
        ...v,
        moqPricing: v.moqPricing.map((row, ri) =>
          ri === rowIndex ? { ...row, [field]: value } : row
        ),
      });
    }));
  };

  const selectedVariant = variants[selectedIndex];
  const rows = selectedVariant?.moqPricing ?? [];

  return (
    <div className="flex flex-col gap-6">
      <div className="grid grid-cols-3 gap-5">
        <SearchDropdown
          label="Currency end price"
          value={data.currencyEndPrice}
          onChange={(v) => onChange('currencyEndPrice', v)}
          placeholder="Search currency..."
          options={currencyOptions}
        />
        <FormInput
          label="Art. nr start cost"
          value={data.artNrStartCost}
          onChange={(v) => onChange('artNrStartCost', v)}
          placeholder="Art.nr for startcost"
        />
        <FormInput
          label="Amount start cost"
          value={data.amountStartCost}
          onChange={(v) => onChange('amountStartCost', v)}
          placeholder="0"
          type="number"
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
              className={`flex flex-col gap-1 px-4 py-3 rounded-lg text-left transition-colors ${
                selectedIndex === i
                  ? 'bg-green-500/10 border border-green-500 text-white'
                  : 'bg-wizard-input border border-wizard-border text-wizard-muted hover:border-white hover:text-white'
              }`}
            >
              <span className={`text-sm font-semibold ${selectedIndex === i ? 'text-green-400' : ''}`}>
                {v.productNumber}
              </span>
              <span className="text-xs">{v.name || '—'}</span>
              <span className="text-xs">{v.supplierArtNr || '—'}</span>
              {v.moqCustomer && (
                <span className="text-xs text-wizard-muted">MOQ: <span className="text-white">{v.moqCustomer}</span></span>
              )}
            </button>
          ))}
        </div>
      </div>

      {/* MOQ Pricing */}
      <div>
        <div className="flex justify-between items-center mb-4">
          <h3 className="text-white font-bold text-base">MOQ Pricing</h3>
          {variants.length > 1 && (
            <div className="flex items-center gap-2">
              <button
                type="button"
                onClick={() => setShowAllVariants(v => !v)}
                className={`flex items-center gap-1.5 px-3 py-1.5 rounded-lg border text-xs font-medium transition-colors ${
                  showAllVariants
                    ? 'bg-white/5 border-white/40 text-white'
                    : 'border-wizard-border text-wizard-muted hover:border-white/40 hover:text-white'
                }`}
              >
                <svg width="13" height="13" viewBox="0 0 13 13" fill="none">
                  <rect x="0.5" y="0.5" width="4" height="4" rx="0.75" stroke="currentColor" />
                  <rect x="0.5" y="8.5" width="4" height="4" rx="0.75" stroke="currentColor" />
                  <rect x="8.5" y="0.5" width="4" height="4" rx="0.75" stroke="currentColor" />
                  <rect x="8.5" y="8.5" width="4" height="4" rx="0.75" stroke="currentColor" />
                </svg>
                View all
              </button>
              <button
                type="button"
                onClick={() => setSyncAll(v => !v)}
                className={`flex items-center gap-2 px-3 py-1.5 rounded-lg border text-xs font-medium transition-colors ${
                  syncAll
                    ? 'bg-green-500/10 border-green-500 text-green-400'
                    : 'border-wizard-border text-wizard-muted hover:border-white hover:text-white'
                }`}
              >
                <span className={`w-3.5 h-3.5 rounded-sm border flex items-center justify-center transition-colors ${
                  syncAll ? 'bg-green-500 border-green-500' : 'border-wizard-muted'
                }`}>
                  {syncAll && (
                    <svg width="8" height="6" viewBox="0 0 8 6" fill="none">
                      <path d="M1 3l2 2 4-4" stroke="black" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
                    </svg>
                  )}
                </span>
                Apply changes to all variants
              </button>
            </div>
          )}
        </div>

        {/* All variants overview card */}
        {showAllVariants && (
          <div className="bg-wizard-input border border-wizard-border rounded-xl p-4 flex flex-col gap-3 mb-5">
            {variants.map((v, i) => (
              <div key={i}>
                {i > 0 && <div className="border-t border-wizard-border mb-3" />}
                <div className="flex items-start justify-between gap-4">
                  <div className="flex gap-4 items-baseline shrink-0 flex-wrap">
                    <span className={`text-sm font-semibold ${selectedIndex === i ? 'text-green-400' : 'text-white'}`}>
                      {v.productNumber}
                    </span>
                    <span className="text-xs text-wizard-muted">{v.name || '—'}</span>
                    <span className="text-xs text-wizard-muted">{v.supplierArtNr || '—'}</span>
                    {v.moqCustomer && (
                      <span className="text-xs text-wizard-muted">MOQ customer: <span className="text-white">{v.moqCustomer}</span></span>
                    )}
                  </div>
                  <button
                    type="button"
                    onClick={() => { setSelectedIndex(i); setShowAllVariants(false); }}
                    className="shrink-0 text-xs text-wizard-muted hover:text-white border border-wizard-border hover:border-white/40 px-2 py-1 rounded-lg transition-colors"
                  >
                    Edit
                  </button>
                </div>
                {v.moqPricing.length > 0 ? (
                  <table className="w-full text-xs mt-2">
                    <thead>
                      <tr className="text-wizard-muted">
                        <th className="text-left pb-1.5 font-medium w-20">MOQ</th>
                        <th className="text-left pb-1.5 font-medium w-24">Freight</th>
                        <th className="text-left pb-1.5 font-medium w-16">Type</th>
                        <th className="text-left pb-1.5 font-medium w-28">End Cust. Price</th>
                        <th className="text-left pb-1.5 font-medium">
                          Net Price{data.supplierCurrency ? ` (${data.supplierCurrency})` : ''}
                        </th>
                      </tr>
                    </thead>
                    <tbody className="divide-y divide-wizard-border">
                      {v.moqPricing.map((row, ri) => (
                        <tr key={ri}>
                          <td className="py-1.5 text-white">{row.moq || '—'}</td>
                          <td className="py-1.5 text-white">{row.freight || '—'}</td>
                          <td className="py-1.5">
                            {row.freightType
                              ? <span className="bg-wizard-badge text-white px-1.5 py-0.5 rounded">{row.freightType}</span>
                              : <span className="text-wizard-muted">—</span>
                            }
                          </td>
                          <td className="py-1.5 text-white">{row.endCustPrice || '—'}</td>
                          <td className="py-1.5 text-white">{row.supplierNetPrice || '—'}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                ) : (
                  <p className="text-wizard-muted text-xs mt-2">No pricing rows</p>
                )}
              </div>
            ))}
          </div>
        )}

        {rows.length > 0 ? (
          <div className="flex flex-col gap-3">
            <div className="grid grid-cols-[1fr_2fr_1fr_1fr_32px] gap-3">
              <span className="text-wizard-muted text-xs font-medium uppercase tracking-wider">MOQ</span>
              <span className="text-wizard-muted text-xs font-medium uppercase tracking-wider">Freight</span>
              <span className="text-wizard-muted text-xs font-medium uppercase tracking-wider flex items-center gap-1.5">
                End. Cust. Price <AutoFilledBadge />
              </span>
              <span className="text-wizard-muted text-xs font-medium uppercase tracking-wider">
                Supplier Net Price{data.supplierCurrency ? ` (${data.supplierCurrency})` : ''}
              </span>
              <span />
            </div>
            {rows.map((row, i) => (
              <div key={i} className="grid grid-cols-[1fr_2fr_1fr_1fr_32px] gap-3 items-center">
                <input type="number" value={row.moq} onChange={(e) => updateRow(i, 'moq', Number(e.target.value))} className={inputClass} />
                <div className="flex gap-2">
                  <input
                    type="number"
                    value={row.freight}
                    onChange={(e) => updateRow(i, 'freight', Number(e.target.value))}
                    className={`${inputClass} flex-1 min-w-0`}
                  />
                  <select
                    value={row.freightType}
                    onChange={(e) => updateRow(i, 'freightType', e.target.value)}
                    className="bg-wizard-input border border-wizard-border rounded-lg px-2 py-2.5 text-white text-sm focus:outline-none focus:border-green-500 transition-colors"
                  >
                    <option value="">—</option>
                    <option value="AIR">AIR</option>
                    <option value="SEA">SEA</option>
                    <option value="TRUCK">TRUCK</option>
                  </select>
                </div>
                <input type="number" value={row.endCustPrice || ''} onChange={(e) => updateRow(i, 'endCustPrice', Number(e.target.value))} placeholder="Suggested price" className={suggestedInputClass} />
                <input type="number" value={row.supplierNetPrice} onChange={(e) => updateRow(i, 'supplierNetPrice', Number(e.target.value))} className={inputClass} />
                <button
                  type="button"
                  onClick={() => removeRow(i)}
                  className="w-8 h-8 flex items-center justify-center rounded-lg border border-red-500/40 text-red-400 hover:bg-red-500/10 hover:border-red-500 transition-colors text-base leading-none"
                >
                  ×
                </button>
              </div>
            ))}
            <button
              type="button"
              onClick={addRow}
              className="mt-1 self-start flex items-center gap-1.5 border border-green-500 text-green-400 hover:bg-green-500/10 px-3 py-1.5 rounded-lg transition-colors text-sm font-medium"
            >
              + Add row
            </button>
          </div>
        ) : (
          <p className="text-wizard-muted text-sm">
            Set "No. of Price levels/Qtys" in Step 1 to generate MOQ rows.
          </p>
        )}
      </div>
    </div>
  );
}
