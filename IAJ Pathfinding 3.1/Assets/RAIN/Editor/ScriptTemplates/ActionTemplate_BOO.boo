<<<<<<< HEAD
﻿import RAIN.Action

[RAINAction]
class ActionTemplate_BOO(RAINAction): 
	def Start(ai as RAIN.Core.AI):
		super.Start(ai)
		return
	
	def Execute(ai as RAIN.Core.AI):
		return ActionResult.SUCCESS

	def Stop(ai as RAIN.Core.AI):
		super.Stop(ai)
=======
﻿import RAIN.Action

[RAINAction]
class ActionTemplate_BOO(RAINAction): 
	def Start(ai as RAIN.Core.AI):
		super.Start(ai)
		return
	
	def Execute(ai as RAIN.Core.AI):
		return ActionResult.SUCCESS

	def Stop(ai as RAIN.Core.AI):
		super.Stop(ai)
>>>>>>> 32342d1e6b1bad9cac424f01f79a5163fc7d6324
		return