import FormInput from '../ui/FormInput';
import FormTextarea from '../ui/FormTextarea';
import FormSelect from '../ui/FormSelect';
import SearchDropdown from '../ui/SearchDropdown';
import { ProductFormData, ProductVariant, getVariantProductNumber } from '../../types/product';
import { suppliers } from '../../types/supplier';

interface Props {
  data: ProductFormData;
  onChange: (field: keyof ProductFormData, value: ProductFormData[keyof ProductFormData]) => void;
  variants: ProductVariant[];
  onVariantsChange: (variants: ProductVariant[]) => void;
}

const lifecycleOptions = [
  { value: 'Active / In Production', label: 'Active / In Production' },
  { value: 'Discontinued', label: 'Discontinued' },
  { value: 'In Development', label: 'In Development' },
];

export default function Step1ProductDetails({ data, onChange, variants, onVariantsChange }: Props) {
  const handleVariantCount = (newCount: number) => {
    const count = Math.max(1, newCount)
    if (count > variants.length) {
      const toAdd = Array.from({ length: count - variants.length }, (_, i) => ({
        variantHead: data.productNumber,
        productNumber: getVariantProductNumber(data.productNumber, variants.length + i),
        name: '',
        antalStaflingar: '',
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
      </div>

      <FormInput
        label="Supplier art. nr / name"
        value={data.supplierArtNr}
        onChange={(v) => onChange('supplierArtNr', v)}
        placeholder="Product name for supplier"
      />

      <div className="grid grid-cols-2 gap-5">
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
        <FormInput
          label="Art. nr start cost"
          value={data.artNrStartCost}
          onChange={(v) => onChange('artNrStartCost', v)}
          placeholder="Art.nr for startcost"
        />
      </div>

      <div className="grid grid-cols-2 gap-5">
        <FormInput
          label="Amount start cost"
          value={data.amountStartCost}
          onChange={(v) => onChange('amountStartCost', v)}
          placeholder="0"
          type="number"
        />
        <FormSelect
          label="Lifecycle"
          value={data.lifecycle}
          onChange={(v) => onChange('lifecycle', v)}
          options={lifecycleOptions}
          placeholder="Select lifecycle"
        />
      </div>

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
            label="Antal stafflingar"
            value={data.antalStaflingar}
            onChange={(v) => onChange('antalStaflingar', v)}
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
            <span className="text-wizard-muted text-xs uppercase tracking-wider">Antal stafflingar</span>
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
                type="number"
                value={variant.antalStaflingar}
                onChange={(e) => updateVariant(i, 'antalStaflingar', e.target.value)}
                placeholder="0"
                className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2 text-white text-sm focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
              />
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
