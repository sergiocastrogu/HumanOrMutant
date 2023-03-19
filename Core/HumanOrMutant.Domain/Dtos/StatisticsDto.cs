﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanOrMutant.Domain.Dtos
{
    public class StatisticsDto
    {
        public int count_mutant_dna { get; set; }
        public int count_human_dna { get; set; }
        public float ratio { get { return (float)count_mutant_dna / count_human_dna; } }
    }
}
