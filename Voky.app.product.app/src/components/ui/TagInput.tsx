import { useState } from 'react';

interface TagInputProps {
  label: string;
  values: string[];
  onAdd: (tag: string) => void;
  onRemove: (tag: string) => void;
  placeholder?: string;
}

export default function TagInput({ label, values, onAdd, onRemove, placeholder }: TagInputProps) {
  const [input, setInput] = useState('');

  const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
    if (e.key === 'Enter' && input.trim()) {
      e.preventDefault();
      onAdd(input.trim());
      setInput('');
    }
  };

  return (
    <div className="flex flex-col">
      <label className="text-white text-sm font-medium mb-1.5 block">{label}</label>
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
          value={input}
          onChange={(e: React.ChangeEvent<HTMLInputElement>) => setInput(e.target.value)}
          onKeyDown={handleKeyDown}
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
      {values.length > 0 && (
        <div className="flex flex-wrap gap-2 mt-2">
          {values.map((tag) => (
            <span
              key={tag}
              className="bg-wizard-badge text-white text-sm px-3 py-1 rounded-full flex items-center gap-1"
            >
              {tag}
              <button
                onClick={() => onRemove(tag)}
                className="text-wizard-muted hover:text-white ml-1 leading-none"
                type="button"
              >
                ×
              </button>
            </span>
          ))}
        </div>
      )}
    </div>
  );
}
