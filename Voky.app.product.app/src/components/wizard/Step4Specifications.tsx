import FormInput from '../ui/FormInput';
import FormTextarea from '../ui/FormTextarea';
import TagInput from '../ui/TagInput';
import AutoFilledBadge from '../ui/AutoFilledBadge';
import { ProductFormData } from '../../types/product';

interface Props {
  data: ProductFormData;
  onChange: (field: keyof ProductFormData, value: ProductFormData[keyof ProductFormData]) => void;
}

export default function Step4Specifications({ data, onChange }: Props) {
  const addTag = (tag: string) => {
    if (!data.tags.includes(tag)) onChange('tags', [...data.tags, tag]);
  };
  const removeTag = (tag: string) => onChange('tags', data.tags.filter((t) => t !== tag));

  const addCategory = (cat: string) => {
    if (!data.categories.includes(cat)) onChange('categories', [...data.categories, cat]);
  };
  const removeCategory = (cat: string) => onChange('categories', data.categories.filter((c) => c !== cat));

  return (
    <div className="flex flex-col gap-5">
      {/* Auto-filled summary */}
      <div className="flex flex-wrap gap-4 pb-4 border-b border-wizard-border">
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
      </div>

      <FormInput
        label="Brand"
        value={data.brand}
        onChange={(v) => onChange('brand', v)}
        placeholder="Brand name"
      />

      <FormTextarea
        label="Delivery information (notes)"
        value={data.deliveryInformation}
        onChange={(v) => onChange('deliveryInformation', v)}
        placeholder="Enter delivery information..."
        rows={4}
      />

      <div className="grid grid-cols-2 gap-5">
        <FormInput
          label="Deliv. time min"
          value={data.delivTimeMin}
          onChange={(v) => onChange('delivTimeMin', v)}
          placeholder="5"
          type="number"
          suffix="working days"
        />
        <FormInput
          label="Deliv. time max"
          value={data.delivTimeMax}
          onChange={(v) => onChange('delivTimeMax', v)}
          placeholder="10"
          type="number"
          suffix="working days"
        />
      </div>

      <FormInput
        label="Size info"
        value={data.sizeInfo}
        onChange={(v) => onChange('sizeInfo', v)}
        placeholder="Standard H: 110cm, W: 65cm, D: 60cm"
      />

      <FormTextarea
        label="Material info"
        value={data.materialInfo}
        onChange={(v) => onChange('materialInfo', v)}
        placeholder="Describe material..."
        rows={3}
      />

      <div className="border-t border-wizard-border pt-5">
        <h3 className="text-white font-bold text-base mb-4">Tags &amp; Categories</h3>
        <div className="grid grid-cols-2 gap-5">
          <TagInput
            label="Tags"
            values={data.tags}
            onAdd={addTag}
            onRemove={removeTag}
            placeholder="Add tags..."
          />
          <TagInput
            label="Categories"
            values={data.categories}
            onAdd={addCategory}
            onRemove={removeCategory}
            placeholder="Add categories..."
          />
        </div>
      </div>
    </div>
  );
}
