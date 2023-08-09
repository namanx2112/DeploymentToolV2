using DeploymentTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace DeploymentTool.Controller
{
    public class AnalyticsController : ApiController
    {
        private dtDBEntities db = new dtDBEntities();

        [Authorize]
        [HttpPost]
        [Route("api/Store/GetProjectPortfolio")]
        public HttpResponseMessage GetProjectPortfolio(Dictionary<string, string> searchFields)
        {
            List<ProjectPortfolio> items = new List<ProjectPortfolio>()
            {
                new ProjectPortfolio()
                {
                    nProjectId = 1,
                    store = new ProjectPortfolioStore()
                    {
                        tStoreDetails = "2455- Austin TX",
                        dtGoliveDate = DateTime.Now.AddDays(-33),
                        tProjectManager = "Bruce Wayne",
                        tProjectType = "Remodel"
                    },
                    networking = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),
                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    pos = new ProjectPortfolioItems(){
                        dtDate = DateTime.Now.AddDays(-333),
                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    audio = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),
                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    paymentsystem = new ProjectPortfolioItems()
                    {dtDate = DateTime.Now.AddDays(-333), tStatus = "Completed", tVendor = "Chomecase"},
                    exteriormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333), tStatus = "Completed", tVendor = "Chomecase"},
                    interiormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333), tStatus = "Completed", tVendor = "Chomecase"},
                    sonicradio = new ProjectPortfolioItems() { dtDate = DateTime.Now.AddDays(-333), tStatus = "Completed", tVendor = "Chomecase" },
                    installation = new ProjectPortfolioItems() {dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    notes = new List<ProjectPorfolioNotes>()
                    {
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "dasdo 99 asdoosajm pm daspoidas -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "sadd 99 asdoosajm pm daspoidas -as0d as-fgdfg as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Brguce wayne",
                            tNotesDesc = "gfhfghfgh 99 asdoosajm pm hgfhfg -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        }
                    }
                },
                new ProjectPortfolio()
                {
                    nProjectId = 2,
                    store = new ProjectPortfolioStore()
                    {
                        tStoreDetails = "2455- Austin TX",
                        dtGoliveDate = DateTime.Now.AddDays(-33),
                        tProjectManager = "Bruce Wayne",
                        tProjectType = "Remodel"
                    },
                    networking = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),
                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    pos = new ProjectPortfolioItems(){
                        dtDate = DateTime.Now.AddDays(-333),
                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    audio = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),
                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    paymentsystem = new ProjectPortfolioItems()
                    {dtDate = DateTime.Now.AddDays(-333), tStatus = "Completed", tVendor = "Chomecase"},
                    exteriormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    interiormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    sonicradio = new ProjectPortfolioItems() { dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase" },
                    installation = new ProjectPortfolioItems() {dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    notes = new List<ProjectPorfolioNotes>()
                    {
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "dasdo 99 asdoosajm pm daspoidas -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "sadd 99 asdoosajm pm daspoidas -as0d as-fgdfg as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Brguce wayne",
                            tNotesDesc = "gfhfghfgh 99 asdoosajm pm hgfhfg -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        }
                    }
                },
                new ProjectPortfolio()
                {
                    nProjectId = 3,
                    store = new ProjectPortfolioStore()
                    {
                        tStoreDetails = "2455- Austin TX",
                        dtGoliveDate = DateTime.Now.AddDays(-33),
                        tProjectManager = "Bruce Wayne",
                        tProjectType = "Remodel"
                    },
                    networking = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    pos = new ProjectPortfolioItems(){
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    audio = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    paymentsystem = new ProjectPortfolioItems()
                    {dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    exteriormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    interiormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    sonicradio = new ProjectPortfolioItems() { dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase" },
                    installation = new ProjectPortfolioItems() {dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    notes = new List<ProjectPorfolioNotes>()
                    {
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "dasdo 99 asdoosajm pm daspoidas -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "sadd 99 asdoosajm pm daspoidas -as0d as-fgdfg as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Brguce wayne",
                            tNotesDesc = "gfhfghfgh 99 asdoosajm pm hgfhfg -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        }
                    }
                },
                new ProjectPortfolio()
                {
                    nProjectId = 4,
                    store = new ProjectPortfolioStore()
                    {
                        tStoreDetails = "2455- Austin TX",
                        dtGoliveDate = DateTime.Now.AddDays(-33),
                        tProjectManager = "Bruce Wayne",
                        tProjectType = "Remodel"
                    },
                    networking = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    pos = new ProjectPortfolioItems(){
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    audio = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    paymentsystem = new ProjectPortfolioItems()
                    {dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    exteriormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    interiormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    sonicradio = new ProjectPortfolioItems() { dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase" },
                    installation = new ProjectPortfolioItems() {dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    notes = new List<ProjectPorfolioNotes>()
                    {
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "dasdo 99 asdoosajm pm daspoidas -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "sadd 99 asdoosajm pm daspoidas -as0d as-fgdfg as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Brguce wayne",
                            tNotesDesc = "gfhfghfgh 99 asdoosajm pm hgfhfg -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        }
                    }
                },
                new ProjectPortfolio()
                {
                    nProjectId = 5,
                    store = new ProjectPortfolioStore()
                    {
                        tStoreDetails = "2455- Austin TX",
                        dtGoliveDate = DateTime.Now.AddDays(-33),
                        tProjectManager = "Bruce Wayne",
                        tProjectType = "Remodel"
                    },
                    networking = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    pos = new ProjectPortfolioItems(){
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    audio = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    paymentsystem = new ProjectPortfolioItems()
                    {dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    exteriormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    interiormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    sonicradio = new ProjectPortfolioItems() { dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase" },
                    installation = new ProjectPortfolioItems() {dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    notes = new List<ProjectPorfolioNotes>()
                    {
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "dasdo 99 asdoosajm pm daspoidas -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "sadd 99 asdoosajm pm daspoidas -as0d as-fgdfg as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Brguce wayne",
                            tNotesDesc = "gfhfghfgh 99 asdoosajm pm hgfhfg -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        }
                    }
                },
                new ProjectPortfolio()
                {
                    nProjectId = 6,
                    store = new ProjectPortfolioStore()
                    {
                        tStoreDetails = "2455- Austin TX",
                        dtGoliveDate = DateTime.Now.AddDays(-33),
                        tProjectManager = "Bruce Wayne",
                        tProjectType = "Remodel"
                    },
                    networking = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    pos = new ProjectPortfolioItems(){
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    audio = new ProjectPortfolioItems()
                    {
                        dtDate = DateTime.Now.AddDays(-333),

                        tStatus = "Completed",
                        tVendor = "Chomecase"
                    },
                    paymentsystem = new ProjectPortfolioItems()
                    {dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    exteriormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    interiormenu = new ProjectPortfolioItems(){dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    sonicradio = new ProjectPortfolioItems() { dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase" },
                    installation = new ProjectPortfolioItems() {dtDate = DateTime.Now.AddDays(-333),  tStatus = "Completed", tVendor = "Chomecase"},
                    notes = new List<ProjectPorfolioNotes>()
                    {
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "dasdo 99 asdoosajm pm daspoidas -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Bruce wayne",
                            tNotesDesc = "sadd 99 asdoosajm pm daspoidas -as0d as-fgdfg as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        },
                        new ProjectPorfolioNotes()
                        {
                            tNotesOwner = "Brguce wayne",
                            tNotesDesc = "gfhfghfgh 99 asdoosajm pm hgfhfg -as0d as-d90 as-d sa-d9asdpasdkasd[askdaskd dkasdlkasd sapdoi sad,"
                        }
                    }
                }
            };
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<ProjectPortfolio>>(items, new JsonMediaTypeFormatter())
            };

        }
    }
}
