import { useState } from 'react'
import { ProductFormData, ProductVariant, initialFormData, getVariantProductNumber } from './types/product'
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

export default function App() {
  const [step, setStep] = useState(1)
  const [formData, setFormData] = useState<ProductFormData>(() => ({
    ...initialFormData,
    productNumber: generateWNr(),
  }))
  const [variants, setVariants] = useState<ProductVariant[]>([
    { variantHead: formData.productNumber, productNumber: getVariantProductNumber(formData.productNumber, 0), name: '', antalStaflingar: '', moqPricing: [] },
  ])

  const handleChange = (field: keyof ProductFormData, value: ProductFormData[keyof ProductFormData]) => {
    setFormData(prev => ({ ...prev, [field]: value }))
  }

  const next = () => setStep(s => Math.min(s + 1, 5))
  const back = () => setStep(s => Math.max(s - 1, 1))
  const cancel = () => {
    const newProductNumber = generateWNr()
    setStep(1)
    setFormData({ ...initialFormData, productNumber: newProductNumber })
    setVariants([{ variantHead: newProductNumber, productNumber: getVariantProductNumber(newProductNumber, 0), name: '', antalStaflingar: '', moqPricing: [] }])
  }
  const save = () => alert('Product saved!')

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
      title={step === 5 ? 'Product Overview' : 'Create New Product'}
      step={step}
      totalSteps={4}
      stepLabel={stepLabels[step - 1] ?? ''}
      isConfirmation={step === 5}
      summaryFields={summaryFields}
      onBack={step > 1 ? back : undefined}
      onNext={step < 5 ? next : undefined}
      onCancel={step === 1 ? cancel : undefined}
      nextLabel={step === 4 ? 'Validate' : step === 5 ? 'Save & Publish Product' : 'Next'}
      onSave={step === 5 ? save : undefined}
    >
      {step === 1 && (
        <Step1ProductDetails
          data={formData}
          onChange={handleChange}
          variants={variants}
          onVariantsChange={setVariants}
        />
      )}
      {step === 2 && <Step2AddQuestions data={formData} onChange={handleChange} />}
      {step === 3 && <Step3PricingMatrix data={formData} onChange={handleChange} variants={variants} onVariantsChange={setVariants} />}
      {step === 4 && <Step4Specifications data={formData} onChange={handleChange} />}
      {step === 5 && <Step5Overview data={formData} variants={variants} />}
    </WizardLayout>
  )
}
