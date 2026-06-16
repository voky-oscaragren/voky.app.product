interface SearchDropdownProps {
  label?: string;
  value: string;
  onChange: (v: string) => void;
  placeholder?: string;
}

export default function SearchDropdown({ label, value, onChange, placeholder }: SearchDropdownProps) {
  return (
    <div className="flex flex-col">
      {label && <label className="text-white text-sm font-medium mb-1.5 block">{label}</label>}
      <div className="relative">
        <svg
          className="absolute left-3 top-1/2 -translate-y-1/2 text-wizard-muted pointer-events-none"
          width="16"
          height="16"
          viewBox="0 0 16 16"
          fill="none"
        >
          <circle cx="7" cy="7" r="4.5" stroke="currentColor" strokeWidth="1.5" />
          <path d="M10.5 10.5l2.5 2.5" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" />
        </svg>
        <input
          type="text"
          value={value}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => onChange(e.target.value)}
          placeholder={placeholder}
          className="bg-wizard-input border border-wizard-border rounded-lg pl-9 pr-9 py-2.5 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
        />
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
