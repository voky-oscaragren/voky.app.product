import { useState } from 'react';
import SearchDropdown from '../ui/SearchDropdown';
import { ProductFormData } from '../../types/product';

interface Props {
  data: ProductFormData;
  onChange: (field: keyof ProductFormData, value: ProductFormData[keyof ProductFormData]) => void;
}

export default function Step2AddQuestions({ data, onChange }: Props) {
  const [groupSearch, setGroupSearch] = useState('');
  const [newQuestion, setNewQuestion] = useState('');
  const [newGroup, setNewGroup] = useState('');

  const addQuestion = (text: string) => {
    if (!text.trim()) return;
    onChange('questions', [...data.questions, text.trim()]);
  };

  const addQuestionGroup = (text: string) => {
    if (!text.trim()) return;
    onChange('questionGroup', text);
  };

  const removeQuestion = (index: number) => {
    onChange('questions', data.questions.filter((_, i) => i !== index));
  };

  return (
    <div className="flex flex-col gap-6">
      {/* Top: two panels with OR divider */}
      <div className="flex gap-0 min-h-[220px]">
        {/* Left panel */}
        <div className="flex-1 pr-6">
          <h3 className="text-white font-semibold text-sm mb-3">Choose existing question group</h3>
          <SearchDropdown
            value={groupSearch}
            onChange={setGroupSearch}
            placeholder="Search existing groups..."
          />
        </div>

        {/* OR divider */}
        <div className="flex flex-col items-center justify-center px-4">
          <div className="w-px flex-1 bg-wizard-border" />
          <span className="text-wizard-muted text-xs font-medium py-2">OR</span>
          <div className="w-px flex-1 bg-wizard-border" />
        </div>

        {/* Right panel */}
        <div className="flex-1 pl-6 flex flex-col gap-4">
          <h3 className="text-white font-semibold text-sm">Create new question group</h3>
          <div className="flex flex-col gap-1.5">
            <label className="text-white text-sm font-medium">Add Question</label>
            <input
              type="text"
              value={newQuestion}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => setNewQuestion(e.target.value)}
              placeholder="e.g Size?"
              className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
            />
            <button
              type="button"
              onClick={() => { addQuestion(newQuestion); setNewQuestion(''); }}
              className="border border-green-500 text-green-400 hover:bg-green-500/10 px-4 py-2 rounded-lg transition-colors text-sm font-medium w-fit mt-1"
            >
              Add question
            </button>
          </div>
          <div className="flex flex-col gap-1.5">
            <label className="text-white text-sm font-medium">Add Question group</label>
            <input
              type="text"
              value={newGroup}
              onChange={(e: React.ChangeEvent<HTMLInputElement>) => setNewGroup(e.target.value)}
              placeholder="e.g Fotboll"
              className="bg-wizard-input border border-wizard-border rounded-lg px-3 py-2 text-white text-sm w-full focus:outline-none focus:border-green-500 transition-colors placeholder:text-wizard-muted"
            />
            <button
              type="button"
              onClick={() => { addQuestionGroup(newGroup); setNewGroup(''); }}
              className="border border-green-500 text-green-400 hover:bg-green-500/10 px-4 py-2 rounded-lg transition-colors text-sm font-medium w-fit mt-1"
            >
              Add question group
            </button>
          </div>
        </div>
      </div>

      {/* Added Questions */}
      <div className="border-t border-wizard-border pt-6">
        <h3 className="text-white font-bold text-base mb-3">Added Questions</h3>
        {data.questions.length === 0 && (
          <p className="text-wizard-muted text-sm">No questions added yet.</p>
        )}
        {data.questions.map((q, i) => (
          <div
            key={i}
            className="bg-wizard-input border border-wizard-border rounded-lg px-4 py-3 flex justify-between items-center text-white mb-2 text-sm"
          >
            <span>{q}</span>
            <button
              type="button"
              onClick={() => removeQuestion(i)}
              className="text-wizard-muted hover:text-white transition-colors ml-4 text-lg leading-none"
            >
              ×
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}
