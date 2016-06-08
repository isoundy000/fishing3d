local GameTableManager = require("Tables.FishingGametableManager")

local GameUIManager = class("GameUIManager")

function GameUIManager:showView(viewname,callback)
    self.uiTable_ = GameTableManager:getTable("table_ui")
    if self.uiTable_ then
        local record = self.uiTable_:getRecordByName(viewname)
        if record then
            panelMgr:CreatePanel(record.prefabName, record.scriptName,callback);
        end
    end
end

return GameUIManager
