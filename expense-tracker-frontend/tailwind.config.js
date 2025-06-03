/** @type {import('tailwindcss').Config} */
export default {
  darkMode: 'media',          // ← use prefers-color-scheme
  content: [
    './index.html',
    './src/**/*.{js,ts,jsx,tsx,vue}',
  ],
  theme: {
    extend: {
      opacity: ['peer-placeholder-shown'],
      
      colors: {
        // light‐mode variables
        background:      'var(--color-background)',
        'background-soft': 'var(--color-background-soft)',
        border:          'var(--color-border)',
        heading:         'var(--color-heading)',
        text:            'var(--color-text)',

        // dark‐mode variables (prefixed “dark:” in your templates)
        'background-dark':      'var(--color-background)',
        'background-soft-dark': 'var(--color-background-soft)',
        'border-dark':          'var(--color-border)',
        'heading-dark':         'var(--color-heading)',
        'text-dark':            'var(--color-text)',

        //brand blue
        'logo-blue':      '#0083F0',
        'logo-blue-dark': '#0016A8',
      },

      // match your body typography
      fontFamily: {
        sans: [
          'Inter',
          'system-ui',
          '-apple-system',
          'Segoe UI',
          'Roboto',
          'Oxygen',
          'Ubuntu',
          'Cantarell',
          'Fira Sans',
          'Droid Sans',
          'Helvetica Neue',
          'sans-serif',
        ],
      },
      fontSize: {
        base: '15px',
      },
      lineHeight: {
        relaxed: '1.6',
      },
    },
  },
  plugins: [],
}
