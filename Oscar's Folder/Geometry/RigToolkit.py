import maya.cmds as cmds
import pymel.core as pm

class WrestlerToolkit(object) :
    attributeHolder = None
    componentRoot = None
    snapAttributes = None
    fkSnapObjs = None
    ikTwistSnapObjs = None
    ikPvSnapObjs = None
    attrDict = None
    uwindow = None
    radioBtnDict = {}
    checkboxDict = {}
    
    def createUI(self) :
        self.uwindow = pm.window(t = 'Rig Toolkit', mnb = False, mxb = False, rtf = True, tlb = True, h= 5)
        pm.columnLayout('mainColumn')
        pm.gridLayout(numberOfColumns=2, cellWidthHeight=(60,25))
        pm.button(label = 'Select All', command = self.selectAll)
        pm.button(label = 'Select Limb', command = self.selectLimb)
        pm.button(label = 'Key All', command = self.keyAll)
        pm.button(label = 'Key Limb', command = self.keyLimb)
        pm.button(label = 'Reset All', command = self.resetAll)
        pm.button(label = 'Reset Sel', command = self.resetSelected)
        pm.setParent( 'mainColumn' )
        pm.frameLayout('ikFkFrame', label = 'IK FK Switching', collapsable = True, width = 120, cc = self.updateWindowSize, cl = True)
        pm.gridLayout(numberOfColumns=2, cellWidthHeight=(60,40))
        pm.button (label = 'Switch Sel\nto IK', command = self.switchToIk)
        pm.button (label = 'Switch Sel\nto FK', command = self.switchToFk)
        pm.setParent( 'mainColumn' )
        pm.frameLayout('mirroringFrame', label = 'Mirroring', collapsable = True, width = 120, cc = self.updateWindowSize, cl = True)
        pm.gridLayout(numberOfColumns=2, cellWidthHeight=(60,25))
        pm.button(label = 'All L>R', command = self.mirrorAllLR)
        pm.button(label = 'All R>L', command = self.mirrorAllRL)
        pm.button(label = 'Sel L>R', command = self.mirrorSelectedLR)
        pm.button(label = 'Sel R>L', command = self.mirrorSelectedRL)
        pm.setParent( 'mainColumn' )
        pm.frameLayout('flippingFrame', label = 'Flipping', collapsable = True, width = 120, cc = self.updateWindowSize, cl = True)
        pm.gridLayout(numberOfColumns=2, cellWidthHeight=(60,25))
        pm.button(label = 'Flip All', command = self.flipAll)
        pm.button(label = 'Flip Sel', command = self.flipSelected)
        pm.setParent('mainColumn')
        pm.frameLayout('visibilityFrame', label='Visibility', collapsable=True, width = 120, cc = self.updateWindowSize, cl = True)
        self.checkboxDict['leftArm'] = pm.checkBox(label='Left Arm', cc = self.setVisibilityToUI)
        self.checkboxDict['rightArm'] = pm.checkBox(label='Right Arm', cc = self.setVisibilityToUI)
        self.checkboxDict['leftLeg'] = pm.checkBox(label='Left Leg', cc = self.setVisibilityToUI)
        self.checkboxDict['rightLeg'] = pm.checkBox(label='Right Leg', cc = self.setVisibilityToUI)
        self.checkboxDict['core'] = pm.checkBox(label='Core', cc = self.setVisibilityToUI)
        self.updateCheckboxUI()
        pm.showWindow(self.uwindow)

    def updateWindowSize(self):
        pm.window(self.uwindow, edit = True, height = 3)
        pm.window(self.uwindow, edit=True, height=3)
        pm.window(self.uwindow, edit=True, height=3)

    def updateCheckboxUI(self):
        for attr in self.checkboxDict :
            onOff = pm.getAttr('fk_c_moveAll_anim.' + attr)
            pm.checkBox(self.checkboxDict[attr], edit = True, value = onOff)

    def setVisibilityToUI(self, args):
        for attr in self.checkboxDict :
            pm.setAttr('fk_c_moveAll_anim.' + attr, pm.checkBox(self.checkboxDict[attr], query = True, value = True))

    def selectAll(self, args) :
        pm.select(pm.editDisplayLayerMembers('Foxo_Controls', q = True))

    def selectLimb(self, args):
        animChildren = []
        for obj in pm.selected() :
            componentRoot = self.findComponentRoot(obj)
            if componentRoot != None :
                rootChildren = pm.listRelatives(componentRoot, ad = True, type = 'transform', p = False, f = False)
                for child in rootChildren :
                    childString = str(child)
                    if '_anim' in childString and '|' not in childString and 'Constraint' not in childString:
                        animChildren.append(child)
        pm.select(animChildren)

    def keyLimb(self, args) :
        preselection = pm.ls(sl = True)
        self.selectLimb(1)
        pm.setKeyframe()
        pm.select(preselection)

    def keyAll(self, args) :
        controls = pm.editDisplayLayerMembers('Foxo_Controls', q = True)
        for control in controls :
            pm.setKeyframe(control)

    def resetAll(self, args) :
        controls = pm.editDisplayLayerMembers('Foxo_Controls', q = True)
        for anim in controls :
            self.resetAnim(anim)
            
    def resetSelected(self, args) :
        controls = pm.ls(sl = True)
        for anim in controls :
            self.resetAnim(anim)
            
    def resetAnim(self, anim) :
        if self.attrDict == None :
            self.defineAttrDict()
        attrDict = self.attrDict
        keyableAttrs = pm.listAttr(anim, keyable = True)
        for keyableAttr in keyableAttrs :
            if keyableAttr in attrDict :
                pm.setAttr(anim + '.' + keyableAttr, attrDict[keyableAttr])
    
    def defineAttrDict(self) :
        attrDict = {}
        attrDict['rotateX'] = 0
        attrDict['rotateY'] = 0
        attrDict['rotateZ'] = 0
        attrDict['translateX'] = 0
        attrDict['translateY'] = 0
        attrDict['translateZ'] = 0
        attrDict['scaleX'] = 1
        attrDict['scaleY'] = 1
        attrDict['scaleZ'] = 1
        attrDict['stretch'] = 0
        attrDict['stretchStart'] = 0
        attrDict['kneeTwist'] = 0
        attrDict['elbowTwist'] = 0
        attrDict['footRoll'] = 0
        attrDict['side'] = 0
        attrDict['toeSpin'] = 0
        attrDict['toeWiggle'] = 0
        attrDict['toeSplay'] = 0
        attrDict['follow'] = 0
        attrDict['curlAllFingers'] = 0
        attrDict['curlIndex'] = 0
        attrDict['curlMiddle'] = 0
        attrDict['curlRing'] = 0
        attrDict['curlPinky'] = 0
        attrDict['curlThumb'] = 0
        attrDict['rampSide'] = 0
        attrDict['splay'] = 0
        attrDict['cup'] = 0
        attrDict['relax'] = 0
        self.attrDict = attrDict

    def flipSelected(self, args) :
        selection = pm.selected()
        for obj in selection :
            self.flipAnim(obj)

    def flipAll(self, args) :
        controls = pm.editDisplayLayerMembers('Foxo_Controls', q = True)
        toFlipControls = []
        for control in controls:
            if '_r_' not in str(control):
                toFlipControls.append(control)
        for control in toFlipControls:
            self.flipAnim(control)

    def flipAnim(self, anim) :
        mirrorAnim = None
        if '_l_' in str(anim) :
            mirrorAnim = pm.PyNode(str(anim).replace('_l_', '_r_'))
        elif '_r_' in str(anim) :
            mirrorAnim = pm.PyNode(str(anim).replace('_r_', '_l_'))
        else :
            mirrorAnim = anim
        if mirrorAnim != None :
            attrDict = self.smartMirrorAttrs(anim)
            mirrorAttrDict = self.smartMirrorAttrs(mirrorAnim)
            for attr in attrDict:
                pm.setAttr(mirrorAnim + '.' + attr, attrDict[attr])
            for attr in mirrorAttrDict :
                pm.setAttr(anim + '.' + attr, mirrorAttrDict[attr])

    def smartMirrorAttrs(self, anim):
        mirrorAttrDict = {}
        animString = str(anim)
        if 'ik' in animString and 'foot' in animString or 'Horselink' in animString:
            mirrorAttrDict = self.getMirroredAttrs(anim, ['translateZ', 'rotateX', 'rotateY'])
        elif 'ik' in animString and 'hand' in animString:
            mirrorAttrDict = self.getMirroredAttrs(anim, ['translateX', 'rotateY', 'rotateZ', 'elbowTwist'])
        elif 'head' in animString or 'jaw' in animString or 'neck' in animString or 'tail' in animString :
            mirrorAttrDict = self.getMirroredAttrs(anim, ['translateZ', 'rotateX', 'rotateY'])
        elif '_c_' in animString :
            mirrorAttrDict = self.getMirroredAttrs(anim, ['translateX', 'rotateZ', 'rotateY'])
        else :
            for attr in pm.listAttr(anim, keyable=True):
                mirrorAttrDict[attr] = pm.getAttr(anim + '.' + attr)
        return mirrorAttrDict

    def getMirroredAttrs(self, anim, flipList):
        mirrorAttrDict = {}
        for attr in pm.listAttr(anim, keyable=True):
            mirrorAttrDict[attr] = pm.getAttr(anim + '.' + attr)
        for attr in flipList:
            if attr in mirrorAttrDict :
                mirrorAttrDict[attr] = mirrorAttrDict[attr] * -1
        return mirrorAttrDict

    def mirrorAllLR(self, args):
        controls = pm.editDisplayLayerMembers('Foxo_Controls', q=True)
        leftControls = []
        for control in controls:
            if '_l_' in str(control):
                leftControls.append(control)
        for control in leftControls:
            self.mirrorAnim(control)

    def mirrorSelectedLR(self, args) :
        selection = pm.selected()
        for obj in selection :
            self.mirrorAnim(pm.PyNode(str(obj).replace('_r_', '_l_')))

    def mirrorSelectedRL(self, args) :
        selection = pm.selected()
        for obj in selection :
            self.mirrorAnim(pm.PyNode(str(obj).replace('_l_', '_r_')))

    def mirrorAllRL(self, args):
        controls = pm.editDisplayLayerMembers('Foxo_Controls', q=True)
        leftControls = []
        for control in controls:
            if '_r_' in str(control):
                leftControls.append(control)
        for control in leftControls:
            self.mirrorAnim(control)

    def mirrorAnim(self, anim):
        mirrorAnim = None
        if '_l_' in str(anim):
            mirrorAnim = pm.PyNode(str(anim).replace('_l_', '_r_'))
        elif '_r_' in str(anim):
            mirrorAnim = pm.PyNode(str(anim).replace('_r_', '_l_'))
        else :
            mirrorAnim = anim
        if mirrorAnim != None:
            attrDict = self.smartMirrorAttrs(anim)
            for attr in attrDict :
                pm.setAttr(mirrorAnim + '.' + attr, attrDict[attr])

    def findComponentRoot(self, anim):
        selection = anim
        heirarchy = pm.listRelatives(selection, ap=True)
        componentRoot = None
        count = 0
        while componentRoot == None :
            count += 1
            if count>200:
                break
            attributes = pm.listAttr(selection, userDefined = True)
            if attributes == None:
                heirarchy = pm.listRelatives(selection, ap = True)
                selection = heirarchy
            else :
                if 'componentRoot' in attributes :
                    componentRoot = selection
                else :
                    heirarchy = pm.listRelatives(selection, ap = True)
                    selection = heirarchy
        return componentRoot

    def findAttributeHolder(self) :
        selection = pm.selected()
        heirarchy = pm.listRelatives(selection, ap = True)
        componentRoot = None
        attributeHolder = None
        while componentRoot == None :
            attributes = pm.listAttr(selection, userDefined = True)
            if attributes == None:
                heirarchy = pm.listRelatives(selection, ap = True)
                selection = heirarchy
            else :
                if 'componentRoot' in attributes :
                    componentRoot = selection
                else :
                    heirarchy = pm.listRelatives(selection, ap = True)
                    selection = heirarchy
        
        if componentRoot!= None :
            self.componentRoot = componentRoot
            children = pm.listRelatives(componentRoot, c = True)
            for x in children :
                attributes = pm.listAttr(x, userDefined = True)
                if attributes == None: 
                    continue
                else :
                    if 'attributeHolder' in attributes :
                        attributeHolder = x
                        self.attributeHolder = attributeHolder
        else :
            self.componentRoot = componentRoot
            pm.error('Component Root not found')
            
        if attributeHolder == None :
            self.attributeHolder = attributeHolder
            pm.error('Attribute Holder not found')

    def updateAttributes(self) :
        attributes = []
        attributes = pm.listAttr(self.attributeHolder, userDefined = True)
        snapAttributes = []
        for x in attributes :
            if 'snap' in x :
                snapAttributes.append(x)
        fkSnapObjs = []
        ikTwistSnapObjs = []
        ikPvSnapObjs = []
        for x in snapAttributes :
            if 'Fk' in x :
                fkSnapObjs.append(x)
            elif 'IkTwist' in x :
                ikTwistSnapObjs.append(x)
            elif 'IkPv' in x :
                ikPvSnapObjs.append(x)
        self.snapAttributes = snapAttributes
        self.fkSnapObjs = fkSnapObjs
        self.ikTwistSnapObjs = ikTwistSnapObjs
        self.ikPvSnapObjs = ikPvSnapObjs

    def armSnapToIk(self) :
        self.updateAttributes()
        attributeHolder = self.attributeHolder
        if 'snapFkUpr' in self.snapAttributes and 'snapFkUprTarget' in self.snapAttributes :
            fkUprTarget = pm.listConnections(attributeHolder + '.snapFkUprTarget')
            fkUprTarget = fkUprTarget[0]
            targetRotate = pm.getAttr(fkUprTarget + '.rotate')
            stretch = pm.getAttr(fkUprTarget +'.snapStretch')
            
            fkUpr = pm.listConnections(attributeHolder + '.snapFkUpr')
            fkUpr = fkUpr[0]
            pm.setAttr(fkUpr + '.rotate', targetRotate)
            pm.setAttr(fkUpr + '.stretch', stretch)
            
        if 'snapFkLwr' in self.snapAttributes and 'snapFkLwrTarget' in self.snapAttributes :
            fkLwrTarget = pm.listConnections(attributeHolder + '.snapFkLwrTarget')
            fkLwrTarget = fkLwrTarget[0]
            targetRotate = pm.getAttr(fkLwrTarget + '.rotate')
            stretch = pm.getAttr(fkLwrTarget +'.snapStretch')
            
            fkLwr = pm.listConnections(attributeHolder + '.snapFkLwr')
            fkLwr = fkLwr[0]
            pm.setAttr(fkLwr + '.rotate', targetRotate)
            pm.setAttr(fkLwr + '.stretch', stretch)
            
        if 'snapFkHand' in self.snapAttributes and 'snapFkHandTarget' in self.snapAttributes :
            fkHandTarget = pm.listConnections(attributeHolder + '.snapFkHandTarget')
            fkHandTarget = fkHandTarget[0]
            targetRotate = pm.getAttr(fkHandTarget + '.rotate')
            
            fkHand = pm.listConnections(attributeHolder + '.snapFkHand')
            fkHand = fkHand[0]
            pm.setAttr(fkHand + '.rotate', targetRotate)

    def armSnapToFk(self) :
        self.updateAttributes()
        attributeHolder = self.attributeHolder
        if 'snapIkHand' in self.snapAttributes and 'snapIkHandTarget' in self.snapAttributes:
            fkHandTarget = pm.listConnections(attributeHolder + '.snapIkHandTarget')
            fkHandTranslate = pm.xform(fkHandTarget, q = True, t = True, ws = True)
            fkHandRotate = pm.xform(fkHandTarget, q = True, ro = True, ws = True)
            
            ikHand = pm.listConnections(attributeHolder + '.snapIkHand')
            pm.xform(ikHand, t = fkHandTranslate, ws = True)
            pm.xform(ikHand, ro = fkHandRotate, ws = True)
        
        if 'snapFkPoleVector' in self.snapAttributes and 'snapIkPoleVector' in self.snapAttributes:
            fkPv = pm.listConnections(attributeHolder + '.snapFkPoleVector')
            fkPvTranslate = pm.xform(fkPv, q = True, t = True, ws = True)
            fkPvRotate = pm.xform(fkPv, q = True, ro = True, ws = True)
        
            ikPv = pm.listConnections(attributeHolder + '.snapIkPoleVector')
            pm.xform(ikPv, t = fkPvTranslate, ws = True)
            pm.xform(ikPv, ro = fkPvRotate, ws = True)
        
        if 'snapFkTwist' in self.snapAttributes and 'snapIkHand' in self.snapAttributes :
            twist = pm.getAttr(attributeHolder + '.snapFkTwist')
            ikHand = pm.listConnections(attributeHolder + '.snapIkHand')
            ikHand = ikHand[0]
            pm.setAttr(ikHand + '.elbowTwist', twist)
            
    def legSnapToIk(self) :
        self.updateAttributes()
        attributeHolder = self.attributeHolder
        if 'snapFkUpr' in self.snapAttributes and 'snapFkUprTarget' in self.snapAttributes :
            fkUprTarget = pm.listConnections(attributeHolder + '.snapFkUprTarget')
            fkUprTarget = fkUprTarget[0]
            targetRotate = pm.getAttr(fkUprTarget + '.rotate')
            stretch = pm.getAttr(fkUprTarget +'.snapStretch')
            
            fkUpr = pm.listConnections(attributeHolder + '.snapFkUpr')
            fkUpr = fkUpr[0]
            pm.setAttr(fkUpr + '.rotate', targetRotate)
            pm.setAttr(fkUpr + '.stretch', stretch)
            
        if 'snapFkLwr' in self.snapAttributes and 'snapFkLwrTarget' in self.snapAttributes :
            fkLwrTarget = pm.listConnections(attributeHolder + '.snapFkLwrTarget')
            fkLwrTarget = fkLwrTarget[0]
            targetRotate = pm.getAttr(fkLwrTarget + '.rotate')
            stretch = pm.getAttr(fkLwrTarget +'.snapStretch')
            
            fkLwr = pm.listConnections(attributeHolder + '.snapFkLwr')
            fkLwr = fkLwr[0]
            pm.setAttr(fkLwr + '.rotate', targetRotate)
            pm.setAttr(fkLwr + '.stretch', stretch)
            
        if 'snapFkFoot' in self.snapAttributes and 'snapFkFootTarget' in self.snapAttributes :
            fkFootTarget = pm.listConnections(attributeHolder + '.snapFkFootTarget')
            fkFootTarget = fkFootTarget[0]
            targetRotate = pm.getAttr(fkFootTarget + '.rotate')
            
            fkFoot = pm.listConnections(attributeHolder + '.snapFkFoot')
            fkFoot = fkFoot[0]
            pm.setAttr(fkFoot + '.rotate', targetRotate)
            
        if 'snapFkToe' in self.snapAttributes and 'snapFkToeTarget' in self.snapAttributes :
            fkToeTarget = pm.listConnections(attributeHolder + '.snapFkToeTarget')
            fkToeTarget = fkToeTarget[0]
            targetRotate = pm.getAttr(fkToeTarget + '.rotate')
            
            fkToe = pm.listConnections(attributeHolder + '.snapFkToe')
            fkToe = fkToe[0]
            pm.setAttr(fkToe + '.rotate', targetRotate)
            
        if 'snapFkHorselink' in self.snapAttributes and 'snapFkHorselinkTarget' in self.snapAttributes :
            fkHorselinkTarget = pm.listConnections(attributeHolder + '.snapFkHorselinkTarget')
            fkHorselinkTarget = fkHorselinkTarget[0]
            targetRotate = pm.getAttr(fkHorselinkTarget + '.rotate')
            
            fkHorselink = pm.listConnections(attributeHolder + '.snapFkHorselink')
            fkHorselink = fkHorselink[0]
            pm.setAttr(fkHorselink + '.rotate', targetRotate)
            
        toeAttributes = ['snapFkToeInner1', 'snapFkToeInner2',
                        'snapFkToeMid1', 'snapFkToeMid2',
                        'snapFkToeOuter1', 'snapFkToeOuter2']
        
        for toeAttr in toeAttributes :
            if toeAttr in self.snapAttributes and toeAttr + 'Target' in self.snapAttributes :
                target = pm.listConnections(attributeHolder + '.' + toeAttr + 'Target')[0]
                targetRotate = pm.getAttr(target + '.rotate')
                control = pm.listConnections(attributeHolder + '.' + toeAttr)[0]
                pm.setAttr(control + '.rotate', targetRotate)
            
    def legSnapToFk(self) :
        self.updateAttributes()
        attributeHolder = self.attributeHolder

        if 'snapFkTwist' in self.snapAttributes and 'snapIkFoot' in self.snapAttributes :
            twist = pm.getAttr(attributeHolder + '.snapFkTwist')
            ikFoot = pm.listConnections(attributeHolder + '.snapIkFoot')
            ikFoot = ikFoot[0]
            pm.setAttr(ikFoot + '.kneeTwist', twist)

        if 'snapIkFoot' in self.snapAttributes and 'snapIkFootTarget' in self.snapAttributes:
            fkFootTarget = pm.listConnections(attributeHolder + '.snapIkFootTarget')
            fkFootTranslate = pm.xform(fkFootTarget, q = True, t = True, ws = True)
            fkFootRotate = pm.xform(fkFootTarget, q = True, ro = True, ws = True)
            
            ikFoot = pm.listConnections(attributeHolder + '.snapIkFoot')
            pm.xform(ikFoot, t = fkFootTranslate, ws = True)
            pm.xform(ikFoot, ro = fkFootRotate, ws = True)

        
        if 'snapFkPoleVector' in self.snapAttributes and 'snapIkPoleVector' in self.snapAttributes:
            fkPv = pm.listConnections(attributeHolder + '.snapFkPoleVector')
            fkPvTranslate = pm.xform(fkPv, q = True, t = True, ws = True)
            fkPvRotate = pm.xform(fkPv, q = True, ro = True, ws = True)
        
            ikPv = pm.listConnections(attributeHolder + '.snapIkPoleVector')
            pm.xform(ikPv, t = fkPvTranslate, ws = True)
            pm.xform(ikPv, ro = fkPvRotate, ws = True)

        if 'snapIkToeWiggle' in self.snapAttributes and 'snapIkFoot' in self.snapAttributes :
            toeWiggle = pm.getAttr(attributeHolder + '.snapIkToeWiggle' )
            ikFoot = pm.listConnections(attributeHolder + '.snapIkFoot')
            ikFoot = ikFoot[0]
            pm.setAttr(ikFoot + '.toeWiggle', toeWiggle)
            
        if 'snapIkFoot' in self.snapAttributes : 
            ikFoot = pm.listConnections(attributeHolder + '.snapIkFoot')
            ikFoot = ikFoot[0]
            pm.setAttr(ikFoot + '.footRoll', 0)
            pm.setAttr(ikFoot + '.side', 0)
            pm.setAttr(ikFoot + '.toeSpin', 0)
            
        if 'snapIkHorselink' in self.snapAttributes and 'snapIkHorselinkTarget' in self.snapAttributes :
            ikHorselinkTarget = pm.listConnections(attributeHolder + '.snapIkHorselinkTarget')
            ikHorselinkTarget = ikHorselinkTarget[0]
            targetRotate = pm.getAttr(ikHorselinkTarget + '.rotate')
            
            ikHorselink = pm.listConnections(attributeHolder + '.snapIkHorselink')
            ikHorselink = ikHorselink[0]
            pm.setAttr(ikHorselink + '.rotate', targetRotate)
            
        toeAttributes = ['snapIkToeInner1', 'snapIkToeInner2',
                        'snapIkToeMid1', 'snapIkToeMid2',
                        'snapIkToeOuter1', 'snapIkToeOuter2']
        for toeAttr in toeAttributes :
            if toeAttr in self.snapAttributes and toeAttr + 'Target' in self.snapAttributes :
                target = pm.listConnections(attributeHolder + '.' + toeAttr + 'Target')[0]
                targetRotate = pm.getAttr(target + '.rotate')
                control = pm.listConnections(attributeHolder + '.' + toeAttr)[0]
                pm.setAttr(control + '.rotate', targetRotate)            
            
    def snapToIk(self, args) :
        self.findAttributeHolder()
        if 'leg' in str(self.attributeHolder) :
            self.legSnapToIk()
        elif 'arm' in str(self.attributeHolder) :
            self.armSnapToIk()
            
    def snapToFk(self, args) :
        self.findAttributeHolder()
        if 'leg' in str(self.attributeHolder) :
            self.legSnapToFk()
        elif 'arm' in str(self.attributeHolder) :
            self.armSnapToFk()
            
    def switchToIk(self, args) :
        self.findAttributeHolder()
        if 'leg' in str(self.attributeHolder) :
            self.legSnapToFk()
            allChildren = pm.listRelatives(self.componentRoot, allDescendents = True)
            for obj in allChildren :
                if obj.count('ik') > 0 :
                    if obj.count('foot_anim') > 0 :
                        pm.select(obj)
        elif 'arm' in str(self.attributeHolder) :
            self.armSnapToFk()
            allChildren = pm.listRelatives(self.componentRoot, allDescendents = True)
            print allChildren
            for obj in allChildren :
                if obj.count('ik') > 0 :
                    if obj.count('hand_anim') > 0 :
                        pm.select(obj)
        pm.setAttr(self.attributeHolder + '.ikFkBlend', 0)
        
    def switchToFk(self, args) :
        self.findAttributeHolder()
        if 'leg' in str(self.attributeHolder) :
            self.legSnapToIk()
            allChildren = pm.listRelatives(self.componentRoot, allDescendents = True)
            for obj in allChildren :
                if obj.count('fk') > 0 :
                    if obj.count('foot_anim') > 0 :
                        pm.select(obj)
        elif 'arm' in str(self.attributeHolder) :
            self.armSnapToIk()
            allChildren = pm.listRelatives(self.componentRoot, allDescendents = True)
            print allChildren
            for obj in allChildren :
                if obj.count('fk') > 0 :
                    if obj.count('hand_anim') > 0 :
                        pm.select(obj)
        pm.setAttr(self.attributeHolder + '.ikFkBlend', 1)
            
toolkit = WrestlerToolkit()
toolkit.createUI()