import { useState } from 'react';
import { QueuedProduct } from '../types/product';

interface Props {
  queue: QueuedProduct[];
  onNewProduct: () => void;
  onEditFromQueue: (index: number) => void;
}

export default function StartPage({ queue, onNewProduct, onEditFromQueue }: Props) {
  const [queueOpen, setQueueOpen] = useState(false);

  return (
    <div className="min-h-screen bg-wizard-bg flex flex-col items-center justify-center p-8">
      <div className="w-full max-w-3xl flex flex-col gap-10">

        {/* Logo + heading */}
        <div className="flex flex-col items-center gap-3">
          <img src="/Voky_green_png.avif" alt="Voky" className="h-10" />
          <h1 className="text-3xl font-bold text-white">Product Manager</h1>
          <p className="text-wizard-muted text-sm">What would you like to do?</p>
        </div>

        {/* Choice cards */}
        <div className="grid grid-cols-3 gap-4">

          {/* Add new product */}
          <button
            type="button"
            onClick={onNewProduct}
            className="flex flex-col items-start gap-4 bg-wizard-card border border-wizard-border hover:border-green-500/60 rounded-2xl p-6 text-left transition-colors group"
          >
            <span className="w-11 h-11 flex items-center justify-center rounded-xl bg-green-500/10 border border-green-500/30 text-green-400 group-hover:bg-green-500/20 transition-colors">
              <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
                <path d="M10 4v12M4 10h12" stroke="currentColor" strokeWidth="2" strokeLinecap="round" />
              </svg>
            </span>
            <div>
              <p className="text-white font-semibold text-sm">Add new product</p>
              <p className="text-wizard-muted text-xs mt-1 leading-relaxed">Start the wizard to create a new product from scratch</p>
            </div>
          </button>

          {/* Edit product in queue */}
          <button
            type="button"
            onClick={() => setQueueOpen(v => !v)}
            className={`flex flex-col items-start gap-4 bg-wizard-card border rounded-2xl p-6 text-left transition-colors group ${
              queueOpen ? 'border-white/40' : 'border-wizard-border hover:border-white/40'
            }`}
          >
            <div className="w-full flex items-start justify-between">
              <span className="w-11 h-11 flex items-center justify-center rounded-xl bg-wizard-input border border-wizard-border text-wizard-muted group-hover:text-white transition-colors">
                <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
                  <path d="M4 6h12M4 10h8M4 14h10" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" />
                </svg>
              </span>
              {queue.length > 0 && (
                <span className="bg-green-500 text-black text-xs font-bold px-2 py-0.5 rounded-full">
                  {queue.length}
                </span>
              )}
            </div>
            <div>
              <p className="text-white font-semibold text-sm">Edit product in queue</p>
              <p className="text-wizard-muted text-xs mt-1 leading-relaxed">
                {queue.length === 0 ? 'No products waiting for approval' : `${queue.length} product${queue.length === 1 ? '' : 's'} waiting for approval`}
              </p>
            </div>
          </button>

          {/* Bulk from Excel — placeholder */}
          <div className="flex flex-col items-start gap-4 bg-wizard-card border border-wizard-border rounded-2xl p-6 opacity-50 cursor-not-allowed">
            <div className="w-full flex items-start justify-between">
              <span className="w-11 h-11 flex items-center justify-center rounded-xl bg-wizard-input border border-wizard-border text-wizard-muted">
                <svg width="20" height="20" viewBox="0 0 20 20" fill="none">
                  <rect x="3" y="2" width="14" height="16" rx="2" stroke="currentColor" strokeWidth="1.5" />
                  <path d="M7 7h6M7 10h6M7 13h4" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" />
                </svg>
              </span>
              <span className="bg-wizard-input border border-wizard-border text-wizard-muted text-xs px-2 py-0.5 rounded-full">
                Soon
              </span>
            </div>
            <div>
              <p className="text-white font-semibold text-sm">Bulk from Excel</p>
              <p className="text-wizard-muted text-xs mt-1 leading-relaxed">Import multiple products at once from a spreadsheet</p>
            </div>
          </div>

        </div>

        {/* Queue list — expands below cards */}
        {queueOpen && (
          <div className="bg-wizard-card border border-wizard-border rounded-2xl overflow-hidden">
            <div className="px-6 py-4 border-b border-wizard-border">
              <p className="text-white font-semibold text-sm">Products awaiting approval</p>
            </div>
            {queue.length === 0 ? (
              <div className="px-6 py-8 text-center">
                <p className="text-wizard-muted text-sm">No products in the queue yet.</p>
              </div>
            ) : (
              <div className="divide-y divide-wizard-border">
                {queue.map((item, i) => (
                  <div key={i} className="flex items-center justify-between px-6 py-4 hover:bg-white/5 transition-colors">
                    <div className="flex items-center gap-5">
                      <span className="text-white font-mono text-sm font-medium">{item.formData.productNumber}</span>
                      <span className="text-white text-sm">{item.formData.name || '—'}</span>
                      <span className="text-wizard-muted text-xs">{item.variants.length} variant{item.variants.length === 1 ? '' : 's'}</span>
                    </div>
                    <button
                      type="button"
                      onClick={() => onEditFromQueue(i)}
                      className="text-sm text-wizard-muted hover:text-white border border-wizard-border hover:border-white/40 px-3 py-1.5 rounded-lg transition-colors"
                    >
                      Edit
                    </button>
                  </div>
                ))}
              </div>
            )}
          </div>
        )}

      </div>
    </div>
  );
}
