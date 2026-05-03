import React from 'react';

interface CardProps {
  title?: string;
  headerRight?: React.ReactNode;
  children: React.ReactNode;
  className?: string;
}

const Card: React.FC<CardProps> = ({ title, headerRight, children, className = '' }) => (
  <div className={`bg-surface border border-border rounded-card p-5 flex flex-col gap-4 ${className}`}>
    {(title || headerRight) && (
      <div className="flex items-center justify-between flex-wrap gap-2">
        {title && <span className="text-[15px] font-bold">{title}</span>}
        {headerRight}
      </div>
    )}
    {children}
  </div>
);

export default Card;
