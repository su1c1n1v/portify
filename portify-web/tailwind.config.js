/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{js,jsx,ts,tsx}'],
  theme: {
    extend: {
      colors: {
        bg:       '#0d0f14',
        surface:  '#161a24',
        surface2: '#1e2330',
        border:   '#2a2f3e',
        accent:   '#6c63ff',
        accent2:  '#00c9a7',
        danger:   '#ff5370',
        warning:  '#ffd166',
        muted:    '#7a8399',
        'pie-tech':   '#6c63ff',
        'pie-health': '#00c9a7',
        'pie-ind':    '#ffd166',
        'pie-etf':    '#4fc3f7',
        'pie-cons':   '#ff8a65',
        'pie-com':    '#ba68c8',
      },
      fontFamily: {
        sans: ['Segoe UI', 'system-ui', 'sans-serif'],
      },
      borderRadius: {
        card: '12px',
      },
    },
  },
  plugins: [],
}

