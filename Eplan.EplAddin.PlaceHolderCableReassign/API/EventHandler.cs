using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Eplan.EplApi.ApplicationFramework;
using Eplan.EplApi.HEServices;
using Eplan.EplApi.DataModel.Graphics;
using Eplan.EplApi.DataModel.EObjects;
using Eplan.EplApi.DataModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace Eplan.EplAddin.PlaceHolderCableReassign.API
{
    /// <summary>
    /// Class that reacts on Eplan events . 
    /// </summary>
    class Eventhandler
    {

        /// <summary>
        /// Produces and initializes the event processing.  
        /// </summary>
        public Eventhandler()
        {

            Eplan.EplApi.ApplicationFramework.EventHandler myHandler = new
                Eplan.EplApi.ApplicationFramework.EventHandler();

            myHandler.SetEvent("onActionEnd.String.XMPlaceHolderAssignRecordAction");
            myHandler.EplanEvent += new EventHandlerFunction(PlaceHolderAssign_EplanEvent);

        }

        /// <summary>
        /// That is the function that is called to the event processing.  
        /// </summary>
        /// <param name="iEventParameter">Event parameter</param>
        private void PlaceHolderAssign_EplanEvent(IEventParameter iEventParameter)
        {
            //Check the selection, and if we have one or more placeholders selected,
            //iterate over their "assigned objects", and when we find cables,
            //call "Reassign All" on them.

            SelectionSet sel = new SelectionSet();

            var PHs = sel.Selection.OfType<PlaceHolder>();
            if (PHs.Count() == 0)
                return;

            CableService CblService = new CableService();

            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                foreach (PlaceHolder ph in PHs)
                {

                    var cables = ph.AssignedObjects.OfType<Cable>().Where(c => c.IsMainFunction);

                    foreach (Cable cbl in cables)
                    {
                        CblService.DoReassignWires(cbl, true, null);
                    }
                }
            }
            catch (Exception)
            {
                //Do nothing
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }

        }
    }
}
