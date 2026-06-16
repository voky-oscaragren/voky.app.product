/** @type {import('tailwindcss').Config} */
export default {
  content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
  theme: {
    extend: {
      colors: {
        wizard: {
          bg: '#0d1a10',
          card: '#122318',
          input: '#0e1c13',
          border: '#1e3525',
          'border-focus': '#22c55e',
          muted: '#6b8a72',
          badge: '#1e3525',
        },
      },
    },
  },
  plugins: [],
}
