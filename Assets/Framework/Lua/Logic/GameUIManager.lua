local CoinAnimationView = require("View.CoinAnimationView")

local GameTableManager = require("Tables.FishingGameTableManager")

local GameUIManager = class("GameUIManager")

function GameUIManager:ctor()
    --ui Root
    self.uiRoot_ = GameObject.FindWithTag("UIRoot")
    --visible ui map
    self.activeViewMap = {}
end

function GameUIManager:getInstance()
    if self.instance_ == nil then
        self.instance_ = GameUIManager.new()
    end
    return self.instance_
end

function GameUIManager:showView(viewname,callback)
    if self.activeViewMap[viewname] then
        if self.activeViewMap[viewname].activeInHierarchy == false then
            self.activeViewMap[viewname]:setActive(true)
        else
            return
        end
    end
    self.uiTable_ = GameTableManager:getTable("table_ui")
    if self.uiTable_ then
        local record = self.uiTable_:getRecordByName(viewname)
        if record then
            ResourceManager:CreateObject("ui",record.prefabName, record.scriptName,"UI",callback,handler(self,self.viewObjCreatedCallback));
        end
    end
    
end

function GameUIManager:viewObjCreatedCallback(obj)
    self.activeViewMap[obj.name] = obj
end

function GameUIManager:hideView(viewname)
    for key,value in pairs(self.activeViewMap) do
        if key == viewname then
            value:Destroy()
            self.activeViewMap[viewname] = nil
        end
    end
end

function GameUIManager:hideAll()
    for key,value in pairs(self.activeViewMap) do
        value:Destroy()
        self.activeViewMap[key] = nil
    end
end

function GameUIManager:getView(viewName)
    
end

function GameUIManager:showCoinAnimationView()
    self.coinAnimationView_ = CoinAnimationView.new()
end

function GameUIManager:getCoinAnimationView()
    return self.coinAnimationView_
end

return GameUIManager
