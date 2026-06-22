import { useState, useEffect } from 'react';
import { ProductFormData, QuestionGroup, GroupQuestion, QuestionAddon, QuestionType, QUESTION_TYPES } from '../../types/product';
import { suppliers } from '../../types/supplier';

interface Props {
  data: ProductFormData;
  onChange: (field: keyof ProductFormData, value: ProductFormData[keyof ProductFormData]) => void;
  existingGroups: QuestionGroup[];
}

interface AddonSelection {
  answerName: string;
  answerNameEn: string;
}

interface CustomAnswer {
  name: string;
  nameEn: string;
}

function questionsMatch(a: GroupQuestion[], b: GroupQuestion[]): boolean {
  if (a.length !== b.length) return false;
  return a.every((qa, i) => {
    const qb = b[i];
    if (qa.text !== qb.text || qa.addons.length !== qb.addons.length) return false;
    return qa.addons.every((addon, j) => {
      const other = qb.addons[j];
      if (addon.addonName === null && other.addonName === null)
        return addon.answerName === other.answerName;
      return addon.addonName === other.addonName;
    });
  });
}

const inputClass =
  'bg-wizard-input border border-wizard-border rounded-md px-2.5 py-1.5 text-white text-xs w-full focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted';

const selectClass =
  'bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors appearance-none cursor-pointer';

export default function Step2AddQuestions({ data, onChange, existingGroups }: Props) {
  const [groupNameInput, setGroupNameInput] = useState(data.questionGroup?.name ?? '');
  const [questionText, setQuestionText] = useState('');
  const [questionTextEn, setQuestionTextEn] = useState('');
  const [questionType, setQuestionType] = useState<QuestionType>('dropdown-no-repeat');
  const [mandatory, setMandatory] = useState(false);
  const [nextQuestion, setNextQuestion] = useState('');
  const [selectedAddons, setSelectedAddons] = useState<Map<string, AddonSelection>>(new Map());
  const [customAnswers, setCustomAnswers] = useState<CustomAnswer[]>([{ name: '', nameEn: '' }]);
  const [addonSearch, setAddonSearch] = useState('');
  const [duplicateGroup, setDuplicateGroup] = useState<QuestionGroup | null>(null);

  useEffect(() => {
    if (!data.questionGroup) {
      onChange('questionGroup', { name: '', questions: [] });
    }
  }, []);

  const headSupplier = suppliers.find(s => s.supplierNr === data.headSupplier);
  const filteredAddons = (headSupplier?.addons ?? []).filter(a =>
    a.name.toLowerCase().includes(addonSearch.toLowerCase())
  );

  const group: QuestionGroup = data.questionGroup ?? { name: '', questions: [] };
  const setGroup = (g: QuestionGroup) => onChange('questionGroup', g);

  const toggleAddon = (name: string) => {
    setSelectedAddons(prev => {
      const next = new Map(prev);
      if (next.has(name)) {
        next.delete(name);
      } else {
        next.set(name, { answerName: name, answerNameEn: '' });
      }
      return next;
    });
  };

  const updateAddonField = (addonName: string, field: keyof AddonSelection, value: string) => {
    setSelectedAddons(prev => {
      const next = new Map(prev);
      const current = next.get(addonName)!;
      next.set(addonName, { ...current, [field]: value });
      return next;
    });
  };

  const addCustomAnswer = () => setCustomAnswers(prev => [...prev, { name: '', nameEn: '' }]);
  const updateCustomAnswer = (i: number, field: keyof CustomAnswer, value: string) =>
    setCustomAnswers(prev => prev.map((a, idx) => idx === i ? { ...a, [field]: value } : a));
  const removeCustomAnswer = (i: number) =>
    setCustomAnswers(prev => prev.filter((_, idx) => idx !== i));

  const totalSelected =
    selectedAddons.size + customAnswers.filter(a => a.name.trim()).length;

  const addQuestion = () => {
    if (!questionText.trim()) return;
    const addons: QuestionAddon[] = [
      ...Array.from(selectedAddons.entries()).map(([addonName, { answerName, answerNameEn }]) => ({
        addonName,
        answerName,
        answerNameEn,
      })),
      ...customAnswers
        .filter(a => a.name.trim())
        .map(({ name, nameEn }) => ({
          addonName: null,
          answerName: name.trim(),
          answerNameEn: nameEn.trim(),
        })),
    ];
    const id = `Q${Math.random().toString(36).slice(2, 6).toUpperCase()}`;
    setGroup({ ...group, questions: [...group.questions, { id, text: questionText.trim(), textEn: questionTextEn.trim(), type: questionType, mandatory, nextQuestion, addons }] });
    setQuestionText('');
    setQuestionTextEn('');
    setQuestionType('dropdown-no-repeat');
    setMandatory(false);
    setNextQuestion('');
    setSelectedAddons(new Map());
    setCustomAnswers([{ name: '', nameEn: '' }]);
    setAddonSearch('');
  };

  const removeQuestion = (index: number) => {
    setGroup({ ...group, questions: group.questions.filter((_, i) => i !== index) });
  };

  const saveName = () => {
    const trimmed = groupNameInput.trim();
    if (!trimmed) return;
    if (group.questions.length > 0) {
      const match = existingGroups.find(g => questionsMatch(g.questions, group.questions));
      if (match) { setDuplicateGroup(match); return; }
    }
    setGroup({ ...group, name: trimmed });
  };

  const typeLabel = (t: QuestionType) => QUESTION_TYPES.find(qt => qt.value === t)?.label ?? t;

  return (
    <div className="flex flex-col gap-6">
      {/* Duplicate group modal */}
      {duplicateGroup && (
        <div className="fixed inset-0 bg-black/60 flex items-center justify-center z-50">
          <div className="bg-wizard-card border border-wizard-border rounded-xl p-6 max-w-md w-full flex flex-col gap-4 mx-4">
            <h3 className="text-white font-semibold text-sm">Group already exists</h3>
            <p className="text-wizard-muted text-sm">
              A question group named <span className="text-white font-medium">"{duplicateGroup.name}"</span> already
              has these exact questions. Do you want to use the existing group or create a new one?
            </p>
            <div className="flex items-center gap-3">
              <button
                type="button"
                onClick={() => { setGroup(duplicateGroup); setGroupNameInput(duplicateGroup.name); setDuplicateGroup(null); }}
                className="border border-green-500 text-green-400 hover:bg-green-500/10 px-4 py-2 rounded-lg transition-colors text-sm font-medium"
              >
                Use existing
              </button>
              <button
                type="button"
                onClick={() => { setGroup({ ...group, name: groupNameInput.trim() }); setDuplicateGroup(null); }}
                className="text-wizard-muted hover:text-white text-sm transition-colors"
              >
                Create new
              </button>
            </div>
          </div>
        </div>
      )}

      {/* Add question form */}
      <div className="flex flex-col gap-4 bg-wizard-input/20 border border-wizard-border rounded-xl p-4">
        <h3 className="text-white font-semibold text-sm">Add a question</h3>

        {/* Question text + English + type */}
        <div className="flex items-end gap-2">
          <div className="flex flex-col gap-1.5 flex-1 min-w-0">
            <label className="text-white text-sm font-medium">Question</label>
            <input
              type="text"
              value={questionText}
              onChange={e => setQuestionText(e.target.value)}
              onKeyDown={e => e.key === 'Enter' && addQuestion()}
              placeholder="e.g. Kan vi lägga till en logga?"
              className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
            />
          </div>
          <div className="flex flex-col gap-1.5 flex-1 min-w-0">
            <label className="text-white text-sm font-medium">Question (English)</label>
            <input
              type="text"
              value={questionTextEn}
              onChange={e => setQuestionTextEn(e.target.value)}
              onKeyDown={e => e.key === 'Enter' && addQuestion()}
              placeholder="e.g. Can we add a logo?"
              className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
            />
          </div>
          <div className="flex flex-col gap-1.5 w-72 shrink-0">
            <label className="text-white text-sm font-medium">Type</label>
            <select
              value={questionType}
              onChange={e => setQuestionType(e.target.value as QuestionType)}
              className={selectClass}
            >
              {QUESTION_TYPES.map(qt => (
                <option key={qt.value} value={qt.value}>{qt.label}</option>
              ))}
            </select>
          </div>
        </div>

        {/* Question choice selector */}
        <div className="flex flex-col gap-2">
          <div className="flex items-center gap-2">
            <label className="text-white text-sm font-medium">Question choice</label>
            <span className="text-wizard-muted text-xs">
              {headSupplier ? `from ${headSupplier.supplierName}` : '— no supplier selected in step 1'}
            </span>
            {totalSelected > 0 && (
              <span className="ml-auto text-xs text-green-400">{totalSelected} selected</span>
            )}
          </div>

          {headSupplier ? (
            <div className="flex flex-col gap-1">
              <div className="relative">
                <svg
                  className="absolute left-3 top-1/2 -translate-y-1/2 text-wizard-muted pointer-events-none"
                  width="13" height="13" viewBox="0 0 16 16" fill="none"
                >
                  <circle cx="7" cy="7" r="4.5" stroke="currentColor" strokeWidth="1.5" />
                  <path d="M10.5 10.5l2.5 2.5" stroke="currentColor" strokeWidth="1.5" strokeLinecap="round" />
                </svg>
                <input
                  type="text"
                  value={addonSearch}
                  onChange={e => setAddonSearch(e.target.value)}
                  placeholder="Search question choices..."
                  className="bg-wizard-input border border-wizard-border rounded-lg pl-8 pr-3 py-2 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
                />
              </div>
              <div className="flex flex-col max-h-72 overflow-y-auto rounded-lg border border-wizard-border bg-wizard-input/30 px-1 py-1">
                {filteredAddons.length === 0 && (
                  <p className="text-wizard-muted text-xs px-3 py-2">No question choices match.</p>
                )}
                {filteredAddons.map(addon => {
                  const sel = selectedAddons.get(addon.name);
                  const isSelected = !!sel;
                  return (
                    <div key={addon.id} className="rounded-lg transition-colors">
                      <label className="flex items-center gap-3 px-3 py-2 rounded-lg hover:bg-wizard-input cursor-pointer transition-colors">
                        <input
                          type="checkbox"
                          checked={isSelected}
                          onChange={() => toggleAddon(addon.name)}
                          className="accent-green-500 w-4 h-4 flex-shrink-0"
                        />
                        <span className="text-white text-sm">{addon.name}</span>
                        {addon.productNr && (
                          <span className="text-wizard-muted text-xs">{addon.productNr}</span>
                        )}
                        {addon.supplierArtNr && (
                          <span className="text-wizard-muted text-xs">{addon.supplierArtNr}</span>
                        )}
                      </label>
                      {isSelected && sel && (
                        <div className="px-3 pb-2.5 ml-7 grid grid-cols-2 gap-2">
                          <div className="flex flex-col gap-0.5">
                            <span className="text-wizard-muted text-[10px]">Name</span>
                            <input
                              type="text"
                              value={sel.answerName}
                              onChange={e => updateAddonField(addon.name, 'answerName', e.target.value)}
                              placeholder={addon.name}
                              className={inputClass}
                            />
                          </div>
                          <div className="flex flex-col gap-0.5">
                            <span className="text-wizard-muted text-[10px]">English name</span>
                            <input
                              type="text"
                              value={sel.answerNameEn}
                              onChange={e => updateAddonField(addon.name, 'answerNameEn', e.target.value)}
                              placeholder={addon.name}
                              className={inputClass}
                            />
                          </div>
                        </div>
                      )}
                    </div>
                  );
                })}
              </div>
            </div>
          ) : (
            <p className="text-wizard-muted text-xs italic">Select a head supplier in step 1 to see available question choices.</p>
          )}

          {/* Custom answers */}
          <div className="flex flex-col gap-1.5 pt-2 border-t border-wizard-border">
            <span className="text-wizard-muted text-xs">Custom answers</span>
            {customAnswers.map((answer, i) => (
              <div key={i} className="flex items-center gap-2">
                <input
                  type="text"
                  value={answer.name}
                  onChange={e => updateCustomAnswer(i, 'name', e.target.value)}
                  placeholder="e.g. Other, None, Custom colour…"
                  className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2 text-white text-sm flex-1 focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
                />
                <input
                  type="text"
                  value={answer.nameEn}
                  onChange={e => updateCustomAnswer(i, 'nameEn', e.target.value)}
                  placeholder="English name"
                  className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2 text-white text-sm flex-1 focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
                />
                <button
                  type="button"
                  onClick={() => removeCustomAnswer(i)}
                  className="text-wizard-muted hover:text-white transition-colors text-lg leading-none shrink-0"
                >
                  ×
                </button>
              </div>
            ))}
          </div>
          <button
            type="button"
            onClick={addCustomAnswer}
            className="self-start border border-green-500 text-green-400 hover:bg-green-500/10 px-4 py-2 rounded-lg transition-colors text-sm font-medium mt-1"
          >
            + Add custom answer
          </button>

          {/* Mandatory + next question */}
          <div className="flex items-end gap-4 pt-2 border-t border-wizard-border">
            <div className="flex flex-col gap-1.5">
              <label className="text-white text-sm font-medium">Question mandatory</label>
              <div className="flex rounded-lg overflow-hidden border border-wizard-border w-fit">
                <button
                  type="button"
                  onClick={() => setMandatory(true)}
                  className={`px-5 py-2.5 text-sm font-medium transition-colors ${
                    mandatory
                      ? 'bg-green-500/20 text-green-400 border-r border-green-500/40'
                      : 'text-wizard-muted hover:text-white border-r border-wizard-border'
                  }`}
                >
                  Yes
                </button>
                <button
                  type="button"
                  onClick={() => setMandatory(false)}
                  className={`px-5 py-2.5 text-sm font-medium transition-colors ${
                    !mandatory
                      ? 'bg-green-500/20 text-green-400'
                      : 'text-wizard-muted hover:text-white'
                  }`}
                >
                  No
                </button>
              </div>
            </div>

            <div className="flex flex-col gap-1.5 flex-1 min-w-0">
              <label className="text-white text-sm font-medium">
                Next question <span className="text-wizard-muted font-normal">(optional)</span>
              </label>
              <input
                type="text"
                value={nextQuestion}
                onChange={e => setNextQuestion(e.target.value)}
                placeholder="e.g. QABC1"
                className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
              />
            </div>
          </div>
        </div>

        <button
          type="button"
          onClick={addQuestion}
          disabled={!questionText.trim()}
          className="border border-green-500 text-green-400 hover:bg-green-500/10 disabled:opacity-30 disabled:cursor-not-allowed px-4 py-2 rounded-lg transition-colors text-sm font-medium w-fit"
        >
          Add question to group
        </button>
      </div>

      {/* Questions overview */}
      {group.questions.length > 0 && (
        <div className="flex flex-col gap-3">
          <h3 className="text-white font-semibold text-sm">Questions added</h3>
          <div className="flex flex-col gap-2">
            {group.questions.map((q, i) => (
              <div
                key={i}
                className="bg-wizard-input/30 border border-wizard-border rounded-xl px-4 py-3 flex flex-col gap-2"
              >
                <div className="flex items-start justify-between gap-4">
                  <div className="flex flex-col gap-0.5 min-w-0">
                    <div className="flex items-center gap-2 flex-wrap">
                      <span className="text-white text-sm font-medium">{q.text}</span>
                      <span className="text-wizard-muted text-xs font-mono">{q.id}</span>
                      {q.mandatory && (
                        <span className="text-[10px] font-medium bg-green-500/20 text-green-400 px-1.5 py-0.5 rounded">
                          Mandatory
                        </span>
                      )}
                    </div>
                    {q.textEn && <span className="text-wizard-muted text-xs">{q.textEn}</span>}
                    <span className="text-wizard-muted text-xs">{typeLabel(q.type)}</span>
                    {q.nextQuestion && (
                      <span className="text-wizard-muted text-xs">
                        Next: <span className="text-white font-mono">{q.nextQuestion}</span>
                        {' — '}{group.questions.find(gq => gq.id === q.nextQuestion)?.text ?? ''}
                      </span>
                    )}
                  </div>
                  <button
                    type="button"
                    onClick={() => removeQuestion(i)}
                    className="text-wizard-muted hover:text-white transition-colors text-lg leading-none flex-shrink-0 mt-px"
                  >
                    ×
                  </button>
                </div>
                {q.addons.length > 0 ? (
                  <div className="flex flex-wrap gap-1.5">
                    {q.addons.map((a, ai) => (
                      <span
                        key={a.addonName ?? `custom-${ai}`}
                        className="bg-wizard-badge text-white text-xs px-2.5 py-1 rounded-full"
                      >
                        {a.answerName}
                        {a.answerNameEn && a.answerNameEn !== a.answerName && (
                          <span className="opacity-50 ml-1">/ {a.answerNameEn}</span>
                        )}
                      </span>
                    ))}
                  </div>
                ) : (
                  <span className="text-wizard-muted text-xs">No answers</span>
                )}
              </div>
            ))}
          </div>
        </div>
      )}

      {/* Name the group */}
      <div className="flex flex-col gap-3 pt-4 border-t border-wizard-border">
        <div>
          <h3 className="text-white font-semibold text-sm mb-1">Name this question group</h3>
          <p className="text-wizard-muted text-xs">
            Give the group a name to identify it, e.g. "Print options" or "Sizing".
          </p>
        </div>
        <div className="flex items-center gap-3">
          <input
            type="text"
            value={groupNameInput}
            onChange={e => setGroupNameInput(e.target.value)}
            onKeyDown={e => e.key === 'Enter' && saveName()}
            placeholder="e.g. Print options"
            className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2.5 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
          />
          <button
            type="button"
            onClick={saveName}
            disabled={!groupNameInput.trim()}
            className="border border-green-500 text-green-400 hover:bg-green-500/10 disabled:opacity-30 disabled:cursor-not-allowed px-4 py-2 rounded-lg transition-colors text-sm font-medium whitespace-nowrap"
          >
            {group.name ? 'Rename' : 'Save name'}
          </button>
        </div>
        {group.name && (
          <p className="text-xs text-wizard-muted">
            Current name: <span className="text-white font-medium">{group.name}</span>
          </p>
        )}
      </div>
    </div>
  );
}
