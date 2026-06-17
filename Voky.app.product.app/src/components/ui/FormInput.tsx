interface FormInputProps {
  label: string;
  value: string;
  onChange: (v: string) => void;
  placeholder?: string;
  type?: string;
  suffix?: string;
  readOnly?: boolean;
}

export default function FormInput({ label, value, onChange, placeholder, type = 'text', suffix, readOnly }: FormInputProps) {
  return (
    <div className="flex flex-col">
      <label className="text-white text-sm font-medium mb-1.5 block">{label}</label>
      <div className="relative">
        <input
          type={type}
          value={value}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => onChange(e.target.value)}
          placeholder={placeholder}
          readOnly={readOnly}
          className={`bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm w-full focus:outline-none transition-colors placeholder:text-wizard-muted${suffix ? ' pr-28' : ''}${readOnly ? ' opacity-60 cursor-default' : ' focus:border-green-500'}`}
        />
        {suffix && (
          <span className="absolute right-3 top-1/2 -translate-y-1/2 text-wizard-muted text-sm pointer-events-none">
            {suffix}
          </span>
        )}
      </div>
    </div>
  );
}
