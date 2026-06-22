import React from 'react';
import AutoFilledBadge from '../ui/AutoFilledBadge';

interface SummaryField {
  label: string;
  value: string;
  autofilled?: boolean;
}

interface WizardLayoutProps {
  title: string;
  step: number;
  totalSteps: number;
  stepLabel: string;
  isConfirmation?: boolean;
  summaryFields?: SummaryField[];
  onBack?: () => void;
  onNext?: () => void;
  onCancel?: () => void;
  onSave?: () => void;
  nextLabel?: string;
  backLabel?: string;
  children: React.ReactNode;
}

export default function WizardLayout({
  title,
  step,
  totalSteps,
  stepLabel,
  isConfirmation,
  summaryFields,
  onBack,
  onNext,
  onCancel,
  onSave,
  nextLabel = 'Next',
  backLabel = 'Back',
  children,
}: WizardLayoutProps) {
  const handleAction = onSave ?? onNext;

  return (
    <div className="min-h-screen bg-wizard-bg p-6">
      <div className="w-full bg-wizard-card rounded-2xl overflow-hidden">
        {/* Header */}
        <div className="px-8 pt-8 pb-6 border-b border-wizard-border">
          <div className="flex justify-between items-center">
            <h1 className="text-2xl font-bold text-white">{title}</h1>
            <img src="/Voky_green_png.avif" alt="Voky" className="h-8" />
          </div>
          {!isConfirmation && (
            <p className="text-wizard-muted text-sm mt-1">
              Slide {step} of {totalSteps} &bull; {stepLabel}
            </p>
          )}
          {isConfirmation && (
            <p className="text-wizard-muted text-sm mt-1">
              Confirmation &bull; Review your product before saving
            </p>
          )}
          {summaryFields && summaryFields.length > 0 && (
            <div className="flex flex-wrap gap-6 mt-4">
              {summaryFields.map((f) => (
                <div key={f.label} className="flex flex-col">
                  <span className="text-wizard-muted text-xs mb-0.5">{f.label}</span>
                  <div className="flex items-center">
                    <span className="text-white text-sm font-medium bg-wizard-input border border-wizard-border rounded px-3 py-1">
                      {f.value}
                    </span>
                    {f.autofilled && <AutoFilledBadge />}
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>

        {/* Body */}
        <div className="px-8 py-6">{children}</div>

        {/* Footer */}
        <div className="px-8 py-5 border-t border-wizard-border flex justify-between items-center">
          <div className="flex items-center gap-4">
            {onCancel && (
              <button
                onClick={onCancel}
                className="flex items-center gap-2 text-wizard-muted hover:text-white transition-colors text-sm"
                type="button"
              >
                <span>✕</span> Cancel
              </button>
            )}
            {onBack && (
              <button
                onClick={onBack}
                className="flex items-center gap-2 text-wizard-muted hover:text-white transition-colors text-sm"
                type="button"
              >
                <span>‹</span> {backLabel}
              </button>
            )}
          </div>
          {handleAction && (
            <button
              onClick={handleAction}
              className="bg-green-500 hover:bg-green-400 text-black font-semibold px-6 py-2 rounded-lg transition-colors text-sm"
              type="button"
            >
              {nextLabel}
            </button>
          )}
        </div>
      </div>
    </div>
  );
}
