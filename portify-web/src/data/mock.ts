import type { AccountStats, CurrencySummary, Pie, Position } from '../types';

export const CHART_COLORS = {
  accent:  '#6c63ff',
  green:   '#00c9a7',
  red:     '#ff5370',
  yellow:  '#ffd166',
  orange:  '#ff8a65',
  blue:    '#4fc3f7',
  purple:  '#ba68c8',
  border:  '#2a2f3e',
};

export const accountStats: AccountStats = {
  totalValue: 24831.5,
  invested: 22900,
  unrealizedPnL: 1931.5,
  realizedPnL: -142.3,
  unrealizedPnLPct: 8.43,
  realizedPnLPct: -0.62,
};

export const trendData = {
  labels: ['Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May'],
  values: [21200, 21800, 22300, 22900, 23600, 24831],
};

export const currencySummary: CurrencySummary[] = [
    { currency: 'EUR', value: 13420,  percentage: 73.68, color: '#00c9a7' },
  { currency: 'USD', value: 7050, percentage: 17.54, color: '#6c63ff' },
  { currency: 'GBP', value: 3116,  percentage: 3.51, color: '#ffd166' },
  { currency: 'GBX', value: 1245,  percentage: 3.51,  color: '#ff8a65' },
  { currency: 'CHF', value: 1245,  percentage: 1.75,  color: '#ba68c8' },
];

export const positions: Position[] = [
  { ticker: 'AAPL', name: 'Apple Inc.',             isin: 'US0378331005', value: 3241,   quantity: 15,  avgPrice: '$182.40', currentPrice: '$215.70', currency: 'USD', unrealizedPnL: 498.8,  unrealizedPnLPct: 18.2,  portfolioPct: 13.0, pie: 'Technology'  },
  { ticker: 'MSFT', name: 'Microsoft Corp.',         isin: 'US5949181045', value: 2880.6, quantity: 8,   avgPrice: '$308.50', currentPrice: '$360.08', currency: 'USD', unrealizedPnL: 413.1,  unrealizedPnLPct: 16.7,  portfolioPct: 11.6, pie: 'Technology'  },
  { ticker: 'NVDA', name: 'NVIDIA Corp.',            isin: 'US67066G1040', value: 2541.2, quantity: 20,  avgPrice: '$104.20', currentPrice: '$127.06', currency: 'USD', unrealizedPnL: 457.3,  unrealizedPnLPct: 21.9,  portfolioPct: 10.2, pie: 'Technology'  },
  { ticker: 'VWRL', name: 'Vanguard FTSE All-World', isin: 'IE00B3RBWM25', value: 2210,   quantity: 25,  avgPrice: '£78.20',  currentPrice: '£88.40',  currency: 'GBP', unrealizedPnL: 255,    unrealizedPnLPct: 13.0,  portfolioPct: 8.9,  pie: 'ETFs'        },
  { ticker: 'JNJ',  name: 'Johnson & Johnson',       isin: 'US4781601046', value: 1980.4, quantity: 12,  avgPrice: '$148.90', currentPrice: '$165.03', currency: 'USD', unrealizedPnL: 193.6,  unrealizedPnLPct: 10.8,  portfolioPct: 8.0,  pie: 'Health'      },
  { ticker: 'BP',   name: 'BP p.l.c.',               isin: 'GB0007980591', value: 1650.8, quantity: 400, avgPrice: '425p',    currentPrice: '412p',    currency: 'GBX', unrealizedPnL: -52,    unrealizedPnLPct: -3.1,  portfolioPct: 6.6,  pie: 'Commodities' },
  { ticker: 'AMZN', name: 'Amazon.com Inc.',         isin: 'US0231351067', value: 1530.2, quantity: 9,   avgPrice: '$152.10', currentPrice: '$170.02', currency: 'USD', unrealizedPnL: 161.1,  unrealizedPnLPct: 11.8,  portfolioPct: 6.2,  pie: 'Consumer'    },
  { ticker: 'SIE',  name: 'Siemens AG',              isin: 'DE0007236101', value: 1324.5, quantity: 7,   avgPrice: '€172.00', currentPrice: '€189.21', currency: 'EUR', unrealizedPnL: 120.5,  unrealizedPnLPct: 10.0,  portfolioPct: 5.3,  pie: 'Industry'    },
  { ticker: 'UNH',  name: 'UnitedHealth Group',      isin: 'US91324P1021', value: 1210,   quantity: 3,   avgPrice: '$360.00', currentPrice: '$403.33', currency: 'USD', unrealizedPnL: 130,    unrealizedPnLPct: 12.0,  portfolioPct: 4.9,  pie: 'Health'      },
  { ticker: 'GOLD', name: 'Barrick Gold Corp.',       isin: 'CA0679011084', value: 980.3,  quantity: 55,  avgPrice: '$15.80',  currentPrice: '$17.82',  currency: 'USD', unrealizedPnL: 111.1,  unrealizedPnLPct: 12.8,  portfolioPct: 3.9,  pie: 'Commodities' },
];

export const pies: Pie[] = [
  {
    id: 'Technology', status: 'AHEAD', invested: 8662, value: 9920, returnPct: 14.5, dividendGained: 42.10, goalProgress: 87, color: '#6c63ff',
    chartData: [34, 29, 26, 11],
    holdings: [{ ticker: 'AAPL', percentage: 34, name: 'Apple Inc.' }, { ticker: 'MSFT', percentage: 29, name: 'Microsoft Corp.' }, { ticker: 'NVDA', percentage: 26, name: 'NVIDIA Corp.' }, { ticker: 'Other', percentage: 11, name: 'Other' }],
  },
  {
    id: 'Health', status: 'ON_TRACK', invested: 3040, value: 3190, returnPct: 4.9, dividendGained: 88.40, goalProgress: 64, color: '#00c9a7',
    chartData: [62, 38],
    holdings: [{ ticker: 'JNJ', percentage: 62, name: 'Johnson & Johnson' }, { ticker: 'UNH', percentage: 38, name: 'UnitedHealth Group' }],
  },
  {
    id: 'Industry', status: 'ON_TRACK', invested: 2100, value: 2310, returnPct: 10.0, dividendGained: 31.00, goalProgress: 58, color: '#ffd166',
    chartData: [57, 26, 17],
    holdings: [{ ticker: 'SIE', percentage: 57, name: 'Siemens AG' }, { ticker: 'CAT', percentage: 26, name: 'Caterpillar Inc.' }, { ticker: 'HON', percentage: 17, name: 'Honeywell International Inc.' }],
  },
  {
    id: 'ETFs', status: 'AHEAD', invested: 3800, value: 4300, returnPct: 13.2, dividendGained: 120.00, goalProgress: 75, color: '#4fc3f7',
    chartData: [51, 32, 17],
    holdings: [{ ticker: 'VWRL', percentage: 51, name: 'Vanguard FTSE All-World' }, { ticker: 'CSPX', percentage: 32, name: 'iShares Core S&P 500 UCITS ETF' }, { ticker: 'IEMA', percentage: 17, name: 'iShares MSCI Emerging Markets UCITS ETF' }],
  },
  {
    id: 'Consumer', status: 'BEHIND', invested: 2200, value: 2310, returnPct: 5.0, dividendGained: 14.80, goalProgress: 42, color: '#ff8a65',
    chartData: [66, 20, 14],
    holdings: [{ ticker: 'AMZN', percentage: 66, name: 'Amazon.com Inc.' }, { ticker: 'PG', percentage: 20, name: 'Procter & Gamble Co.' }, { ticker: 'COST', percentage: 14, name: 'Costco Wholesale Corp.' }],
  },
  {
    id: 'Commodities', status: 'ON_TRACK', invested: 2900, value: 2800, returnPct: -3.4, dividendGained: 55.20, goalProgress: 70, color: '#ba68c8',
    chartData: [59, 35, 6],
    holdings: [{ ticker: 'BP', percentage: 59, name: 'BP plc' }, { ticker: 'GOLD', percentage: 35, name: 'Barrick Gold Corp.' }, { ticker: 'RIO', percentage: 6, name: 'Rio Tinto Group' }],
  },
];

export const PIE_COLORS: Record<string, string> = {
  Technology:  '#6c63ff',
  Health:      '#00c9a7',
  Industry:    '#ffd166',
  ETFs:        '#4fc3f7',
  Consumer:    '#ff8a65',
  Commodities: '#ba68c8',
};
