﻿using Bunit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bit.BlazorUI.Tests.Buttons;

[TestClass]
public class BitToggleButtonTests : BunitTestContext
{
    [DataTestMethod,
       DataRow(Visual.Fluent, true, true, "Button label", BitIconName.Emoji2, "title"),
       DataRow(Visual.Fluent, true, false, "Button label", BitIconName.Emoji2, "title"),
       DataRow(Visual.Fluent, false, true, "Button label", BitIconName.Emoji2, "title"),
       DataRow(Visual.Fluent, false, false, "Button label", BitIconName.Emoji2, "title"),

       DataRow(Visual.Cupertino, true, true, "Button label", BitIconName.Emoji2, "title"),
       DataRow(Visual.Cupertino, true, false, "Button label", BitIconName.Emoji2, "title"),
       DataRow(Visual.Cupertino, false, true, "Button label", BitIconName.Emoji2, "title"),
       DataRow(Visual.Cupertino, false, false, "Button label", BitIconName.Emoji2, "title"),

       DataRow(Visual.Material, true, true, "Button label", BitIconName.Emoji2, "title"),
       DataRow(Visual.Material, true, false, "Button label", BitIconName.Emoji2, "title"),
       DataRow(Visual.Material, false, true, "Button label", BitIconName.Emoji2, "title"),
       DataRow(Visual.Material, false, false, "Button label", BitIconName.Emoji2, "title"),
   ]
    public void BitToggleButtonShouldHaveCorrectLabelAndIconAndTitle(Visual visual,
        bool isChecked,
        bool isEnabled,
        string label,
        BitIconName? iconName,
        string title)
    {
        var component = RenderComponent<BitToggleButton>(parameters =>
        {
            parameters.AddCascadingValue(visual);
            parameters.Add(p => p.IsChecked, isChecked);
            parameters.Add(p => p.Label, label);
            parameters.Add(p => p.IconName, iconName);
            parameters.Add(p => p.IsEnabled, isEnabled);
            parameters.Add(p => p.Title, title);
        });

        var bitToggleButton = component.Find(".bit-tglb");
        var bitIconTag = component.Find(".bit-tglb > span > i");
        var bitLabelTag = component.Find(".bit-tglb > span > span");

        var isEnabledClass = isEnabled ? "enabled" : "disabled";
        var visualClass = visual == Visual.Cupertino ? "cupertino" : visual == Visual.Material ? "material" : "fluent";

        Assert.IsTrue(bitToggleButton.ClassList.Contains($"bit-tglb-{isEnabledClass}-{visualClass}"));

        Assert.AreEqual(bitLabelTag.TextContent, label);

        Assert.AreEqual(bitToggleButton.GetAttribute("title"), title);

        Assert.IsTrue(bitIconTag.ClassList.Contains($"bit-icon--{iconName.GetName()}"));
    }

    [DataTestMethod,
        DataRow(true),
        DataRow(false),
    ]
    public void BitToggleButtonClickEvent(bool isEnabled)
    {
        var clicked = false;
        var component = RenderComponent<BitToggleButton>(parameters =>
        {
            parameters.Add(p => p.IsEnabled, isEnabled);
            parameters.Add(p => p.OnClick, () => clicked = true);
        });

        var bitToggleButton = component.Find(".bit-tglb");

        bitToggleButton.Click();

        Assert.AreEqual(isEnabled, clicked);
    }

    [DataTestMethod,
        DataRow(true, true),
        DataRow(true, false),
        DataRow(false, true),
        DataRow(false, false)
    ]
    public void BitToggleButtonShouldChangeIsCheckedParameterAfterClickWhenIsEnable(bool isEnabled, bool isChecked)
    {
        var component = RenderComponent<BitToggleButton>(parameters =>
        {
            parameters.Add(p => p.IsEnabled, isEnabled);
            parameters.Add(p => p.IsChecked, isChecked);
        });

        var bitToggleButton = component.Find(".bit-tglb");

        bitToggleButton.Click();

        //TODO: bypassed - BUnit 2-way bound parameters issue
        //Assert.AreEqual(isEnabled ? !isChecked : isChecked, component.Instance.IsChecked);
    }

    [DataTestMethod,
        DataRow(true, true),
        DataRow(true, false),
        DataRow(false, true),
        DataRow(false, false)
    ]
    public void BitToggleButtonShouldAddRomveCheckedClassAfterClickWhenIsEnable(bool isEnabled, bool isChecked)
    {
        var component = RenderComponent<BitToggleButton>(parameters =>
        {
            parameters.Add(p => p.IsEnabled, isEnabled);
            parameters.Add(p => p.IsChecked, isChecked);
        });

        var bitToggleButton = component.Find(".bit-tglb");

        bitToggleButton.Click();

        //TODO: bypassed - BUnit 2-way bound parameters issue
        //Assert.AreEqual(isEnabled ? !isChecked : isChecked, bitToggleButton.ClassList.Contains("bit-tglb-checked"));
    }

    [DataTestMethod,
      DataRow(true, false),
      DataRow(true, true),
      DataRow(false, false),
      DataRow(false, true),
    ]
    public void BitToggleButtonDisabledFocusTest(bool isEnabled, bool allowDisabledFocus)
    {
        var component = RenderComponent<BitToggleButton>(parameters =>
        {
            parameters.Add(p => p.IsEnabled, isEnabled);
            parameters.Add(p => p.AllowDisabledFocus, allowDisabledFocus);
        });

        var bitButton = component.Find(".bit-tglb");
        var hasTabindexAttr = bitButton.HasAttribute("tabindex");

        Assert.AreEqual(!isEnabled && !allowDisabledFocus, hasTabindexAttr);

        if (hasTabindexAttr)
        {
            Assert.IsTrue(bitButton.GetAttribute("tabindex").Equals("-1"));
        }
    }

    [DataTestMethod, DataRow("Detailed description")]
    public void BitToggleButtonAriaDescriptionTest(string ariaDescription)
    {
        var component = RenderComponent<BitToggleButton>(parameters =>
        {
            parameters.Add(p => p.AriaDescription, ariaDescription);
        });

        var bitButton = component.Find(".bit-tglb");

        Assert.IsTrue(bitButton.HasAttribute("aria-describedby"));

        Assert.AreEqual(bitButton.GetAttribute("aria-describedby"), ariaDescription);
    }

    [DataTestMethod, DataRow("Detailed label")]
    public void BitToggleButtonAriaLabelTest(string ariaLabel)
    {
        var component = RenderComponent<BitToggleButton>(parameters =>
        {
            parameters.Add(p => p.AriaLabel, ariaLabel);
        });

        var bitButton = component.Find(".bit-tglb");

        Assert.IsTrue(bitButton.HasAttribute("aria-label"));

        Assert.AreEqual(bitButton.GetAttribute("aria-label"), ariaLabel);
    }

    [DataTestMethod,
        DataRow(true),
        DataRow(false),
        DataRow(null)
    ]
    public void BitToggleButtonAriaHiddenTest(bool ariaHidden)
    {
        var component = RenderComponent<BitToggleButton>(parameters =>
        {
            parameters.Add(p => p.AriaHidden, ariaHidden);
        });

        var bitButton = component.Find(".bit-tglb");

        Assert.AreEqual(ariaHidden ? true : false, bitButton.HasAttribute("aria-hidden"));
    }

    [DataTestMethod,
        DataRow("", true),
        DataRow("bing.com", true),
        DataRow("bing.com", false)
    ]
    public void BitToggleButtonShouldRenderExpectedElementBasedOnHref(string href, bool isEnabled)
    {
        var component = RenderComponent<BitToggleButton>(parameters =>
        {
            parameters.Add(p => p.Href, href);
            parameters.Add(p => p.IsEnabled, isEnabled);
        });

        var bitToggleButton = component.Find(".bit-tglb");
        var tagName = bitToggleButton.TagName;
        var expectedElement = href.HasValue() && isEnabled ? "a" : "button";

        Assert.AreEqual(expectedElement, tagName, ignoreCase: true);
    }

    [DataTestMethod,
        DataRow(true),
        DataRow(false),
        DataRow(null)
    ]
    public void BitToggleButtonDefaultIsCheckedTest(bool? defaultIsChecked)
    {
        bool isCheckedAfterOnChange = false;

        var component = RenderComponent<BitToggleButton>(parameters =>
        {
            parameters.Add(p => p.DefaultIsChecked, defaultIsChecked);
            parameters.Add(p => p.OnChange, (e) => isCheckedAfterOnChange = e);
        });

        var bitToggleButton = component.Find(".bit-tglb");

        bitToggleButton.Click();

        if (defaultIsChecked is null)
        {
            Assert.AreEqual(isCheckedAfterOnChange, true);
        }
        else
        {
            Assert.AreNotEqual(defaultIsChecked, isCheckedAfterOnChange);
        }
    }
}
