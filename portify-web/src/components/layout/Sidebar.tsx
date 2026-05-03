import React from 'react';
import { PIE_COLORS } from '../../data/mock';

const NAV_PIES = ['Technology', 'Health', 'Industry', 'ETFs', 'Consumer', 'Commodities'] as const;

interface NavItemProps {
  icon?: React.ReactNode;
  dot?: string;
  label: string;
  active?: boolean;
}

const NavItem: React.FC<NavItemProps> = ({ icon, dot, label, active }) => (
  <a
    href="#"
    className={`flex items-center gap-2.5 px-2.5 py-2 rounded-lg text-sm transition-colors duration-150 no-underline ${
      active
        ? 'bg-surface2 text-accent'
        : 'text-muted hover:bg-surface2 hover:text-[#e2e8f0]'
    }`}
  >
    {icon && <span className="flex-shrink-0">{icon}</span>}
    {dot && (
      <span className="w-2 h-2 rounded-full flex-shrink-0" style={{ background: dot }} />
    )}
    {label}
  </a>
);

const DashIcon = () => (
  <svg width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2" viewBox="0 0 24 24">
    <rect x="3" y="3" width="7" height="7"/><rect x="14" y="3" width="7" height="7"/>
    <rect x="3" y="14" width="7" height="7"/><rect x="14" y="14" width="7" height="7"/>
  </svg>
);
const PosIcon = () => (
  <svg width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2" viewBox="0 0 24 24">
    <polyline points="22 12 18 12 15 21 9 3 6 12 2 12"/>
  </svg>
);
const RetIcon = () => (
  <svg width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2" viewBox="0 0 24 24">
    <circle cx="12" cy="12" r="10"/><polyline points="12 6 12 12 16 14"/>
  </svg>
);
const AllocIcon = () => (
  <svg width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2" viewBox="0 0 24 24">
    <path d="M12 2L2 7l10 5 10-5-10-5zM2 17l10 5 10-5M2 12l10 5 10-5"/>
  </svg>
);
const CurrIcon = () => (
  <svg width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2" viewBox="0 0 24 24">
    <line x1="12" y1="1" x2="12" y2="23"/><path d="M17 5H9.5a3.5 3.5 0 0 0 0 7h5a3.5 3.5 0 0 1 0 7H6"/>
  </svg>
);
const DivIcon = () => (
  <svg width="16" height="16" fill="none" stroke="currentColor" strokeWidth="2" viewBox="0 0 24 24">
    <polyline points="23 6 13.5 15.5 8.5 10.5 1 18"/><polyline points="17 6 23 6 23 12"/>
  </svg>
);

const Sidebar: React.FC = () => (
  <aside className="w-[220px] min-w-[220px] bg-surface border-r border-border flex flex-col py-6 sticky top-0 h-screen overflow-y-auto">
    {/* Logo */}
    <div className="flex items-center gap-2.5 px-5 pb-7 text-xl font-bold tracking-tight">
      <div className="w-8 h-8 rounded-lg flex items-center justify-center text-base"
        style={{ background: 'linear-gradient(135deg, #6c63ff, #00c9a7)' }}>
        📈
      </div>
      Portify
    </div>

    {/* Overview */}
    <div className="px-3 mb-2">
      <p className="text-[10px] uppercase tracking-widest text-muted px-2.5 py-2">Overview</p>
      <NavItem icon={<DashIcon />} label="Dashboard" active />
      <NavItem icon={<PosIcon />} label="Positions" />
      <NavItem icon={<RetIcon />} label="Returns" />
    </div>

    {/* Pies */}
    <div className="px-3 mb-2">
      <p className="text-[10px] uppercase tracking-widest text-muted px-2.5 py-2">Pies</p>
      {NAV_PIES.map((pie) => (
        <NavItem key={pie} dot={PIE_COLORS[pie]} label={pie} />
      ))}
    </div>

    {/* Analytics */}
    <div className="px-3 mb-2">
      <p className="text-[10px] uppercase tracking-widest text-muted px-2.5 py-2">Analytics</p>
      <NavItem icon={<AllocIcon />} label="Allocation" />
      <NavItem icon={<CurrIcon />} label="Currencies" />
      <NavItem icon={<DivIcon />} label="Dividends" />
    </div>

    {/* Footer */}
    <div className="mt-auto px-5 pt-4 border-t border-border">
      <div className="flex items-center gap-2.5">
        <div className="w-[34px] h-[34px] rounded-full flex items-center justify-center font-bold text-[13px] text-white"
          style={{ background: 'linear-gradient(135deg, #6c63ff, #00c9a7)' }}>
          VC
        </div>
        <div className="leading-tight">
          <p className="font-semibold text-[13px]">Vinicius C.</p>
          <p className="text-[11px] text-muted">EUR Account</p>
        </div>
      </div>
    </div>
  </aside>
);

export default Sidebar;
