using System;
using System.Linq;
using System.Collections.Generic;
	
namespace CRMWebApp.Models
{   
	public  class vw_CustomerContactAndBankListRepository : EFRepository<vw_CustomerContactAndBankList>, Ivw_CustomerContactAndBankListRepository
	{

	}

	public  interface Ivw_CustomerContactAndBankListRepository : IRepository<vw_CustomerContactAndBankList>
	{

	}
}