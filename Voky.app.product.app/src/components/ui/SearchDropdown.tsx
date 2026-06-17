import { useState, useRef, useEffect } from 'react';

export interface DropdownOption {
  value: string;
  label: string;
  sublabel?: string;
}

interface SearchDropdownProps {
  label?: string;
  value: string;
  onChange: (v: string) => void;
  placeholder?: string;
  options?: DropdownOption[];
}

export default function SearchDropdown({ label, value, onChange, placeholder, options = [] }: SearchDropdownProps) {
  const [inputText, setInputText] = useState(() => options.find(o => o.value === value)?.label ?? value)
  const [open, setOpen] = useState(false)
  const containerRef = useRef<HTMLDivElement>(null)

  useEffect(() => {
    setInputText(options.find(o => o.value === value)?.label ?? value)
  }, [value])

  useEffect(() => {
    function handleClickOutside(e: MouseEvent) {
      if (containerRef.current && !containerRef.current.contains(e.target as Node)) {
        setOpen(false)
      }
    }
    document.addEventListener('mousedown', handleClickOutside)
    return () => document.removeEventListener('mousedown', handleClickOutside)
  }, [])

  const filtered = options.filter(o =>
    o.label.toLowerCase().includes(inputText.toLowerCase()) ||
    o.value.toLowerCase().includes(inputText.toLowerCase())
  )

  const handleSelect = (option: DropdownOption) => {
    onChange(option.value)
    setInputText(option.label)
    setOpen(false)
  }

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setInputText(e.target.value)
    setOpen(true)
    if (e.target.value === '') onChange('')
  }

  return (
    <div className="flex flex-col" ref={containerRef}>
      {label && <label className="text-white text-sm font-medium mb-1.5 block">{label}</label>}
      <div className="relative">
        <svg
          className="absolute left-3 top-1/2 -translate-y-1/2 text-wizard-muted pointer-events-none"
          width="16" height="16" viewBox="0 0 16 16" fill="none"
        >
          <circle cx="7" cy="7" r="4.5" stroke="currentColor" strokeWidth="1.5" />
          <path d="M10.5 10.5l2.5 2.5" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" />
        </svg>
        <input
          type="text"
          value={inputText}
          onChange={handleInputChange}
          onFocus={() => setOpen(true)}
          placeholder={placeholder}
          className="bg-wizard-input border border-wizard-border rounded-lg pl-9 pr-9 py-2.5 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
        />
        <svg
          className="absolute right-3 top-1/2 -translate-y-1/2 pointer-events-none text-wizard-muted"
          width="16" height="16" viewBox="0 0 16 16" fill="none"
        >
          <path d="M4 6l4 4 4-4" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" strokeLinejoin="round" />
        </svg>

        {open && filtered.length > 0 && (
          <ul className="absolute z-10 mt-1 w-full bg-wizard-card border border-wizard-border rounded-lg overflow-hidden shadow-lg">
            {filtered.map(option => (
              <li
                key={option.value}
                onMouseDown={() => handleSelect(option)}
                className={`flex items-center justify-between px-3 py-2.5 cursor-pointer hover:bg-wizard-input transition-colors ${
                  value === option.value ? 'bg-wizard-input' : ''
                }`}
              >
                <span className="text-white text-sm">{option.label}</span>
                {option.sublabel && (
                  <span className="text-wizard-muted text-xs ml-3">{option.sublabel}</span>
                )}
              </li>
            ))}
          </ul>
        )}
      </div>
    </div>
  )
}
