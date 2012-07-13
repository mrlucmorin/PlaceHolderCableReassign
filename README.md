PlaceHolderCableReassign
========================

Automatically reassign cable data after selecting a new value set from a PlaceHolder in Eplan P8

See my blog post at http://www.stlm.ca/?p=406 for a description of the problem, or my video at http://www.youtube.com/watch?v=J8Nf2elNzcY&feature=plcp for a demonstration.

This EPlan P8 API Add-In implements an Event Handler to respond to the "onActionEnd.String.XMPlaceHolderAssignRecordAction" event which is fired after the user Assigns a value set to a PlaceHolder object.

The EventHandler is registered with the Eplan event subsystem within the OnInit method of the AddIn class. Once registered, a .NET event handler is registered as the "callback" method to be executed when the event is fired.

The bulk of the code resides in the PlaceHolderAssign_EplanEvent method of the EventHandler class.

In this method, we obtain a reference to the currently selected PlaceHolder object, then we iterate over the cables that are assigned to it, and call the CableService.DoReassignWires method on each one of them.

This will have the same effect as manually using the  “Project Data > Cables > Assign Conductors > Reassign All” menu in Eplan.
