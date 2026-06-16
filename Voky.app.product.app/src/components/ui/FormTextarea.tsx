interface FormTextareaProps {
  label: string;
  value: string;
  onChange: (v: string) => void;
  placeholder?: string;
  rows?: number;
}

export default function FormTextarea({ label, value, onChange, placeholder, rows = 4 }: FormTextareaProps) {
  return (
    <div className="flex flex-col">
      <label className="text-white text-sm font-medium mb-1.5 block">{label}</label>
      <textarea
        value={value}
        onChange={(e: React.ChangeEvent<HTMLTextAreaElement>) => onChange(e.target.value)}
        placeholder={placeholder}
        rows={rows}
        className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted resize-none"
      />
    </div>
  );
}
