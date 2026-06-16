import FormInput from '../ui/FormInput';
import FormTextarea from '../ui/FormTextarea';
import FormSelect from '../ui/FormSelect';
import SearchDropdown from '../ui/SearchDropdown';
import { ProductFormData } from '../../types/product';

interface Props {
  data: ProductFormData;
  onChange: (field: keyof ProductFormData, value: ProductFormData[keyof ProductFormData]) => void;
}

const lifecycleOptions = [
  { value: 'Active / In Production', label: 'Active / In Production' },
  { value: 'Discontinued', label: 'Discontinued' },
  { value: 'In Development', label: 'In Development' },
];

export default function Step1ProductDetails({ data, onChange }: Props) {
  return (
    <div className="flex flex-col gap-5">
      <div className="grid grid-cols-2 gap-5">
        <FormInput
          label="Product number"
          value={data.productNumber}
          onChange={(v) => onChange('productNumber', v)}
          placeholder="Enter product number"
        />
        <FormInput
          label="Name"
          value={data.name}
          onChange={(v) => onChange('name', v)}
          placeholder="Product name"
        />
      </div>

      <div className="grid grid-cols-2 gap-5">
        <FormInput
          label="Supplier art. nr / name"
          value={data.supplierArtNr}
          onChange={(v) => onChange('supplierArtNr', v)}
          placeholder="Supplier details"
        />
        <FormInput
          label="Art. nr varianthead"
          value={data.artNrVarianthead}
          onChange={(v) => onChange('artNrVarianthead', v)}
          placeholder="Variant head number"
        />
      </div>

      <div className="grid grid-cols-2 gap-5">
        <SearchDropdown
          label="Head supplier"
          value={data.headSupplier}
          onChange={(v) => onChange('headSupplier', v)}
          placeholder="Search supplier"
        />
        <FormInput
          label="Art. nr start cost"
          value={data.artNrStartCost}
          onChange={(v) => onChange('artNrStartCost', v)}
          placeholder="Start cost"
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

      <div className="flex items-end gap-6">
        <div className="w-48">
          <FormInput
            label="MOQ Customer"
            value={data.moqCustomer}
            onChange={(v) => onChange('moqCustomer', v)}
            placeholder="0"
            type="number"
          />
        </div>

        <div className="flex flex-col mb-0.5">
          <span className="text-white text-sm font-medium mb-2 block">Send to opti</span>
          <div className="flex items-center gap-4">
            <label className="flex items-center gap-2 text-white cursor-pointer text-sm">
              <span
                onClick={() => onChange('sendToOpti', true)}
                className={`w-5 h-5 rounded-full border-2 flex items-center justify-center cursor-pointer transition-colors ${
                  data.sendToOpti
                    ? 'bg-green-500 border-green-500'
                    : 'border-wizard-muted bg-transparent'
                }`}
              >
                {data.sendToOpti && (
                  <svg width="10" height="8" viewBox="0 0 10 8" fill="none">
                    <path d="M1 4l2.5 2.5L9 1" stroke="black" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
                  </svg>
                )}
              </span>
              Yes
            </label>
            <label className="flex items-center gap-2 text-white cursor-pointer text-sm">
              <span
                onClick={() => onChange('sendToOpti', false)}
                className={`w-5 h-5 rounded-full border-2 flex items-center justify-center cursor-pointer transition-colors ${
                  !data.sendToOpti
                    ? 'border-white'
                    : 'border-wizard-muted bg-transparent'
                }`}
              />
              No
            </label>
          </div>
        </div>

        <div className="ml-auto mb-0.5">
          <button
            type="button"
            className="border border-green-500 text-green-400 hover:bg-green-500/10 px-4 py-2 rounded-lg transition-colors text-sm font-medium"
          >
            Add Child
          </button>
        </div>
      </div>
    </div>
  );
}
