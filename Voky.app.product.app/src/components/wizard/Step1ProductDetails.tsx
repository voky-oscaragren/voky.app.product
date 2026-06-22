import FormInput from '../ui/FormInput';
import FormTextarea from '../ui/FormTextarea';
import SearchDropdown from '../ui/SearchDropdown';
import { ProductFormData, ProductVariant, getVariantProductNumber } from '../../types/product';
import { suppliers } from '../../types/supplier';

interface Props {
  data: ProductFormData;
  onChange: (field: keyof ProductFormData, value: ProductFormData[keyof ProductFormData]) => void;
  variants: ProductVariant[];
  onVariantsChange: (variants: ProductVariant[]) => void;
}

export default function Step1ProductDetails({ data, onChange, variants, onVariantsChange }: Props) {
  const handleVariantCount = (newCount: number) => {
    const count = Math.max(1, newCount)
    if (count > variants.length) {
      const toAdd = Array.from({ length: count - variants.length }, (_, i) => ({
        variantHead: data.productNumber,
        productNumber: getVariantProductNumber(data.productNumber, variants.length + i),
        name: '',
        supplierArtNr: '',
        antalStaflingar: data.antalStaflingar,
        moqCustomer: '',
        moqPricing: [],
      }))
      onVariantsChange([...variants, ...toAdd])
    } else {
      onVariantsChange(variants.slice(0, count))
    }
  }

  const updateVariant = (index: number, field: keyof ProductVariant, value: string) => {
    const updated = variants.map((v, i) => i === index ? { ...v, [field]: value } : v)
    onVariantsChange(updated)
  }

  return (
    <div className="flex flex-col gap-5">
      <div className="grid grid-cols-2 gap-5">
        <FormInput
          label="Name"
          value={data.name}
          onChange={(v) => onChange('name', v)}
          placeholder="Product name"
        />
        <div className="flex flex-col gap-1.5">
          <label className="text-white text-sm font-medium">Is addon</label>
          <div className="flex rounded-lg overflow-hidden border border-wizard-border w-fit">
            <button
              type="button"
              onClick={() => onChange('isAddon', true)}
              className={`px-5 py-2.5 text-sm font-medium transition-colors ${
                data.isAddon
                  ? 'bg-green-500/20 text-green-400 border-r border-green-500/40'
                  : 'text-wizard-muted hover:text-white border-r border-wizard-border'
              }`}
            >
              Yes
            </button>
            <button
              type="button"
              onClick={() => onChange('isAddon', false)}
              className={`px-5 py-2.5 text-sm font-medium transition-colors ${
                !data.isAddon
                  ? 'bg-green-500/20 text-green-400'
                  : 'text-wizard-muted hover:text-white'
              }`}
            >
              No
            </button>
          </div>
        </div>
      </div>

      <FormInput
        label="Supplier Art.Nr/Name"
        value={data.supplierArtNr}
        onChange={(v) => onChange('supplierArtNr', v)}
        placeholder="Product name for supplier"
      />

      <SearchDropdown
        label="Head supplier"
        value={data.headSupplier}
        onChange={(v) => onChange('headSupplier', v)}
        placeholder="Search supplier"
        options={suppliers.map(s => ({
          value: s.supplierNr,
          label: s.supplierName,
          sublabel: s.supplierCurrency,
        }))}
      />

      <FormTextarea
        label="Product description"
        value={data.productDescription}
        onChange={(v) => onChange('productDescription', v)}
        placeholder="Enter detailed product description..."
        rows={4}
      />

      <div className="flex justify-between items-center gap-5">
        <div className="flex-1">
          <FormInput
            label="MOQ Customer"
            value={data.moqCustomer}
            onChange={(v) => onChange('moqCustomer', v)}
            placeholder="0"
            type="number"
          />
        </div>

        <div className="flex-1">
          <FormInput
            label="Variants"
            value={String(variants.length)}
            onChange={(v) => handleVariantCount(parseInt(v, 10) || 1)}
            placeholder="1"
            type="number"
          />
        </div>
      </div>

      {variants.length > 0 && (
        <div className="flex flex-col gap-2 mt-1">
          <div className="grid grid-cols-3 gap-3 px-1">
            <span className="text-wizard-muted text-xs uppercase tracking-wider">Product number</span>
            <span className="text-wizard-muted text-xs uppercase tracking-wider">Product name</span>
            <span className="text-wizard-muted text-xs uppercase tracking-wider">Supplier Art.Nr/name </span>
          </div>
          {variants.map((variant, i) => (
            <div key={i} className="grid grid-cols-3 gap-3 bg-wizard-input/30 border border-wizard-border rounded-lg px-3 py-2.5">
              <div className="flex items-center">
                <span className="text-white text-sm">{variant.productNumber}</span>
              </div>
              <input
                type="text"
                value={variant.name}
                onChange={(e) => updateVariant(i, 'name', e.target.value)}
                placeholder="Product name"
                className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2 text-white text-sm focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
              />
              <input
                type="text"
                value={variant.supplierArtNr}
                onChange={(e) => updateVariant(i, 'supplierArtNr', e.target.value)}
                placeholder="Supplier Art.Nr/Name"
                className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2 text-white text-sm focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
              />
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
