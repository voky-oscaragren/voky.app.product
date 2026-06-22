import { useState } from 'react'
import { ProductFormData, ProductVariant, QueuedProduct, initialFormData, getVariantProductNumber } from './types/product'
import StartPage from './components/StartPage'
import WizardLayout from './components/wizard/WizardLayout'
import Step1ProductDetails from './components/wizard/Step1ProductDetails'
import Step2AddQuestions from './components/wizard/Step2AddQuestions'
import Step3PricingMatrix from './components/wizard/Step3PricingMatrix'
import Step4Specifications from './components/wizard/Step4Specifications'
import Step5Overview from './components/wizard/Step5Overview'

function generateWNr(): string {
  const num = Math.floor(10000 + Math.random() * 90000)
  return `W-${num}`
}

function freshVariants(productNumber: string): ProductVariant[] {
  return [{ variantHead: productNumber, productNumber: getVariantProductNumber(productNumber, 0), name: '', supplierArtNr: '', antalStaflingar: initialFormData.antalStaflingar, moqCustomer: '', moqPricing: [] }]
}

const TEST_QUEUE: QueuedProduct[] = [
  {
    formData: {
      ...initialFormData,
      productNumber: 'W-42001',
      name: 'Bamboo Tote Bag',
      supplierArtNr: 'BTB-100',
      headSupplier: 'SUP-002',
      artNrStartCost: 'SC-001',
      amountStartCost: '250',
      productDescription: 'Eco-friendly bamboo fibre tote bag with reinforced handles and internal zip pocket.',
      moqCustomer: '50',
      antalStaflingar: '3',
      sendToOpti: true,
      questionGroup: {
        name: 'Bags',
        questions: [
          { id: 'Q0001', text: 'Kan vi lägga till en logga?', textEn: 'Can we add a logo?', type: 'dropdown-no-repeat', mandatory: true, nextQuestion: 'Q0002', addons: [{ addonName: 'Laser engraving', answerName: 'Laser engraving', answerNameEn: 'Laser engraving' }, { addonName: 'Digital print', answerName: 'Digital print', answerNameEn: 'Digital print' }] },
          { id: 'Q0002', text: 'Vilka färger finns?', textEn: 'What colours are available?', type: 'dropdown-no-repeat', mandatory: false, nextQuestion: '', addons: [] },
        ],
      },
      currencyEndPrice: 'SEK',
      supplierCurrency: 'EUR',
      brand: 'EcoLine',
      deliveryInformation: 'Ships from warehouse in Rotterdam',
      delivTimeMin: '10',
      delivTimeMax: '15',
      sizeInfo: '38 x 42 cm',
      materialInfo: 'Bamboo fibre blend',
      tags: ['eco', 'bag', 'bamboo'],
      categories: ['Bags & Accessories'],
    },
    variants: [
      {
        variantHead: 'W-42001',
        productNumber: 'W-42002',
        name: 'Natural',
        supplierArtNr: 'BTB-100-NAT',
        antalStaflingar: '3',
        moqCustomer: '50',
        moqPricing: [
          { moq: 50,  freight: 120, freightType: 'SEA',   endCustPrice: 89,  supplierNetPrice: 42 },
          { moq: 100, freight: 100, freightType: 'SEA',   endCustPrice: 79,  supplierNetPrice: 36 },
          { moq: 250, freight: 80,  freightType: 'SEA',   endCustPrice: 69,  supplierNetPrice: 30 },
        ],
      },
      {
        variantHead: 'W-42001',
        productNumber: 'W-42003',
        name: 'Black',
        supplierArtNr: 'BTB-100-BLK',
        antalStaflingar: '3',
        moqCustomer: '50',
        moqPricing: [
          { moq: 50,  freight: 120, freightType: 'SEA',   endCustPrice: 89,  supplierNetPrice: 43 },
          { moq: 100, freight: 100, freightType: 'SEA',   endCustPrice: 79,  supplierNetPrice: 37 },
          { moq: 250, freight: 80,  freightType: 'AIR',   endCustPrice: 69,  supplierNetPrice: 31 },
        ],
      },
    ],
  },
]

export default function App() {
  const [page, setPage] = useState<'home' | 'wizard'>('home')
  const [step, setStep] = useState(1)
  const [formData, setFormData] = useState<ProductFormData>(() => ({ ...initialFormData, productNumber: generateWNr() }))
  const [variants, setVariants] = useState<ProductVariant[]>(() => freshVariants(formData.productNumber))
  const [queue, setQueue] = useState<QueuedProduct[]>(TEST_QUEUE)
  const [editingIndex, setEditingIndex] = useState<number | null>(null)

  const handleChange = (field: keyof ProductFormData, value: ProductFormData[keyof ProductFormData]) => {
    setFormData(prev => ({ ...prev, [field]: value }))
  }

  const startNewProduct = () => {
    const productNumber = generateWNr()
    setFormData({ ...initialFormData, productNumber })
    setVariants(freshVariants(productNumber))
    setEditingIndex(null)
    setStep(1)
    setPage('wizard')
  }

  const startEditing = (index: number) => {
    const item = queue[index]
    setFormData(item.formData)
    setVariants(item.variants)
    setEditingIndex(index)
    setStep(1)
    setPage('wizard')
  }

  const next = () => setStep(s => Math.min(s + 1, 5))
  const back = () => setStep(s => Math.max(s - 1, 1))

  const cancel = () => {
    setPage('home')
    setStep(1)
    setEditingIndex(null)
  }

  const addToQueue = () => {
    const item: QueuedProduct = { formData, variants }
    if (editingIndex !== null) {
      setQueue(q => q.map((qi, i) => i === editingIndex ? item : qi))
    } else {
      setQueue(q => [...q, item])
    }
    setPage('home')
    setStep(1)
    setEditingIndex(null)
  }

  if (page === 'home') {
    return (
      <StartPage
        queue={queue}
        onNewProduct={startNewProduct}
        onEditFromQueue={startEditing}
      />
    )
  }

  const stepLabels = ['Product Details', 'Add Questions', 'Pricing Matrix', 'Specifications']

  const summaryFields1 = [
    { label: 'Product number', value: formData.productNumber, autofilled: true },
  ]
  const summaryFields3 = [
    { label: 'Variant head', value: formData.productNumber, autofilled: true },
    { label: 'Name', value: formData.name || 'Ergonomic Office Chair v4', autofilled: true },
    { label: 'Supplier currency', value: formData.supplierCurrency, autofilled: true },
  ]
  const summaryFields4 = [
    { label: 'Product number', value: formData.productNumber, autofilled: true },
    { label: 'Name', value: formData.name || 'Ergonomic Office Chair v4', autofilled: true },
  ]

  const summaryFields =
    step === 1 ? summaryFields1 :
    step === 3 ? summaryFields3 :
    step === 4 ? summaryFields4 :
    undefined

  return (
    <WizardLayout
      title={step === 5 ? 'Product Overview' : editingIndex !== null ? 'Edit Product' : 'Create New Product'}
      step={step}
      totalSteps={4}
      stepLabel={stepLabels[step - 1] ?? ''}
      isConfirmation={step === 5}
      summaryFields={summaryFields}
      onBack={step > 1 ? back : undefined}
      onNext={step < 5 ? next : undefined}
      onCancel={cancel}
      nextLabel={step === 4 ? 'Validate' : step === 5 ? 'Add to queue' : 'Next'}
      onSave={step === 5 ? addToQueue : undefined}
    >
      {step === 1 && (
        <Step1ProductDetails
          data={formData}
          onChange={handleChange}
          variants={variants}
          onVariantsChange={setVariants}
        />
      )}
      {step === 2 && (
          <Step2AddQuestions
            data={formData}
            onChange={handleChange}
            existingGroups={queue
              .filter((_, i) => i !== editingIndex)
              .flatMap(p => p.formData.questionGroup ? [p.formData.questionGroup] : [])}
          />
        )}
      {step === 3 && <Step3PricingMatrix data={formData} onChange={handleChange} variants={variants} onVariantsChange={setVariants} />}
      {step === 4 && <Step4Specifications data={formData} onChange={handleChange} />}
      {step === 5 && <Step5Overview data={formData} variants={variants} />}
    </WizardLayout>
  )
}
