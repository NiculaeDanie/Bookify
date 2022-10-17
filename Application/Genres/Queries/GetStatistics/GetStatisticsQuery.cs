using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Genres.Queries.GetStatistics
{
    public class GetStatisticsQuery: IRequest<Dictionary<Genre,int>>
    {
    }
}
