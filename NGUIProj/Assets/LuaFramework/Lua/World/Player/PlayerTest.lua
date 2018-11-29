require "World/Player/PlayerModel"
require "World/Player/PlayerEntity"

PlayerTest = class(PlayerEntity)

local this = {}

this.Path = nil
this.PlayMoveSpeed = 30
this.currentWatPoint = 0 
this.stopMove = true
this.playerCenterY = 1
--[[
countLabel表的创建是为了能让c#的awake, ondestroy能够回调到！
表名是prefab的名字，luaframework是根据prefab名进行查询的
]]--
countLabel = {}

this.super = PlayerEntity

this.callback = nil
function PlayerTest:ctor()
	this.super:ctor("PlayerTest")
	this.gameObject = nil;
	this.transform = nil;
end

function PlayerTest:init(Parent)		
	this.PlayerModel = PlayerModel.new(1);
	this.PlayerModel:setPlayerName("TestMianPlayer")
	
	this.gameObject = GameObject.Instantiate(resMgr:LoadPrefab("Player"))	 --GameObject.Find("Player") -- 
	
	this.transform = this.gameObject.transform	

	this.seeker = this.gameObject:GetComponent("Seeker")

	--this.gameObject:AddComponent('Binary')
	this.playerCenterY =this.transform.localPosition.y
	print(this.seeker.name)
	this.transform.parent = Parent
	UpdateBeat:Add(update,self)
	FixedUpdateBeat:Add(FixedUpdate,self)

	this.gameObject:SetActive(true)
	this.callback = self.OnPathComplete
end


function PlayerTest.OnPathComplete(p)
	if p.error == false then
		print("寻路完成"..tostring(p))
		this.currentWatPoint = 0
		this.Path = p
		this.stopMove = false
	end
end

function update()
	
	if UnityEngine.Input.GetMouseButtonDown(0) then
		local _layer = 2 ^ LayerMask.NameToLayer('Ground') 
		local Camera = UnityEngine.Camera.main
		local ray = Camera:ScreenPointToRay(UnityEngine.Input.mousePosition)
		local flag, hit = UnityEngine.Physics.Raycast(ray, nil, 5000, _layer)
		if flag then
			print('pick from lua, point: '..tostring(hit.point))                                        
		end

		this.seeker.pathCallback = this.callback
		this.seeker:StartPath(this.transform.position, hit.point)
	end
end

function FixedUpdate()
	if this.Path == nil or this.stopMove then
		return
	end

	local currentWayPointV = Vector3(this.Path.vectorPath[this.currentWatPoint].x, this.Path.vectorPath[this.currentWatPoint].y + this.playerCenterY, this.Path.vectorPath[this.currentWatPoint].z)
	--print(currentWayPointV)
	local dir = (currentWayPointV - this.transform.position).normalized
	dir = dir * this.PlayMoveSpeed * Time.fixedDeltaTime;
	local offset = Vector3.Distance(this.transform.localPosition, currentWayPointV)
	--print(offset)

	if offset < 0.1 then
        this.transform.localPosition = currentWayPointV

    	this.currentWatPoint = this.currentWatPoint + 1

        if (this.currentWatPoint == this.Path.vectorPath.Count) then
           
            this.stopMove = true

            this.currentWatPoint = 0
            this.Path = nil
		end  
	else    
        if (dir.magnitude > offset) then
          
        local tmpV3 = dir * (offset / dir.magnitude)
            dir = tmpV3

			this.currentWatPoint = this.currentWatPoint + 1

      	  if (this.currentWatPoint == this.Path.vectorPath.Count) then
				this.stopMove = true
				this.currentWatPoint = 0
				this.Path = nil
			end
        end
		this.transform.localPosition = this.transform.localPosition + dir
        end
end

