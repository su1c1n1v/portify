import React from 'react';

interface BadgeProps {
  label: string;
  color?: string;
  bg?: string;
}

const Badge: React.FC<BadgeProps> = ({ label, color, bg }) => (
  <span
    className="inline-block px-2 py-0.5 rounded-full text-[11px] font-semibold"
    style={{ color, background: bg }}
  >
    {label}
  </span>
);

export default Badge;
