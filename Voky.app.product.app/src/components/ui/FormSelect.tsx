interface Option {
  value: string;
  label: string;
}

interface FormSelectProps {
  label: string;
  value: string;
  onChange: (v: string) => void;
  options: Option[];
  placeholder?: string;
}

export default function FormSelect({ label, value, onChange, options, placeholder }: FormSelectProps) {
  return (
    <div className="flex flex-col">
      <label className="text-white text-sm font-medium mb-1.5 block">{label}</label>
      <div className="relative">
        <select
          value={value}
          onChange={(e: React.ChangeEvent<HTMLSelectElement>) => onChange(e.target.value)}
          className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors appearance-none cursor-pointer"
        >
          {placeholder && <option value="" className="bg-wizard-card">{placeholder}</option>}
          {options.map((opt) => (
            <option key={opt.value} value={opt.value} className="bg-wizard-card">
              {opt.label}
            </option>
          ))}
        </select>
        <svg
          className="absolute right-3 top-1/2 -translate-y-1/2 pointer-events-none text-wizard-muted"
          width="16"
          height="16"
          viewBox="0 0 16 16"
          fill="none"
        >
          <path d="M4 6l4 4 4-4" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>
      </div>
    </div>
  );
}
