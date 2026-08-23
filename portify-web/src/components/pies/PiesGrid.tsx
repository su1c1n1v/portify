import React from 'react';
import type { Pie } from '../../types';
import PieCard from './PieCard';

interface Props { pies: Pie[]; }

const PiesGrid: React.FC<Props> = ({ pies }) => (
  <div className="grid grid-cols-3 gap-5">
    {pies.map((pie) => (
      <PieCard key={pie.id} pie={pie} />
    ))}
  </div>
);

export default PiesGrid;
