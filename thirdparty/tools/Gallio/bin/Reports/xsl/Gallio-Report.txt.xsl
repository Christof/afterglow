<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:g="http://www.gallio.org/">
  <xsl:param name="resourceRoot" select="''" />
  
  <xsl:param name="show-passed-tests">true</xsl:param>
  <xsl:param name="show-failed-tests">true</xsl:param>
  <xsl:param name="show-inconclusive-tests">true</xsl:param>
  <xsl:param name="show-skipped-tests">true</xsl:param>

  <xsl:output method="text" encoding="utf-8"/>

  <xsl:template match="/">
    <xsl:apply-templates select="//g:report" />
  </xsl:template>

  <xsl:template match="g:report">
		<xsl:apply-templates select="." mode="results"/>
    <xsl:apply-templates select="g:testModel/g:annotations" />
    <xsl:apply-templates select="g:testPackageRun/g:statistics" />
  </xsl:template>
  
	<xsl:template match="g:statistics">
    <xsl:text>* Results: </xsl:text>
    <xsl:call-template name="format-statistics">
      <xsl:with-param name="statistics" select="." />
    </xsl:call-template>
		<xsl:text>&#xA;</xsl:text>
	</xsl:template>
  
  <xsl:template match="g:annotations">
    <xsl:if test="g:annotation">
      <xsl:text>* Annotations:&#xA;&#xA;</xsl:text>
      <xsl:apply-templates select="g:annotation[@type='error']"/>
      <xsl:apply-templates select="g:annotation[@type='warning']"/>
      <xsl:apply-templates select="g:annotation[@type='info']"/>
    </xsl:if>
  </xsl:template>

  <xsl:template match="g:annotation">
    <xsl:call-template name="indent">
      <xsl:with-param name="text">
        <xsl:text>[</xsl:text><xsl:value-of select="@type"/><xsl:text>] </xsl:text>
        <xsl:value-of select="@message"/>
      </xsl:with-param>
      <xsl:with-param name="firstLinePrefix" select="''" />
    </xsl:call-template>

    <xsl:if test="g:codeLocation/@path">
      <xsl:call-template name="indent">
        <xsl:with-param name="text">
          <xsl:text>Location: </xsl:text>
          <xsl:call-template name="format-code-location"><xsl:with-param name="codeLocation" select="g:codeLocation" /></xsl:call-template>
        </xsl:with-param>
        <xsl:with-param name="secondLinePrefix" select="'    '" />
      </xsl:call-template>
    </xsl:if>
      
    <xsl:if test="g:codeReference/@assembly">
      <xsl:call-template name="indent">
        <xsl:with-param name="text">
          <xsl:text>Reference: </xsl:text>
          <xsl:call-template name="format-code-reference"><xsl:with-param name="codeReference" select="g:codeReference" /></xsl:call-template>
        </xsl:with-param>
        <xsl:with-param name="secondLinePrefix" select="'    '" />
      </xsl:call-template>
    </xsl:if>
      
    <xsl:if test="@details">
      <xsl:call-template name="indent">
        <xsl:with-param name="text">
          <xsl:text>Details: </xsl:text>
          <xsl:value-of select="@details"/>
        </xsl:with-param>
        <xsl:with-param name="secondLinePrefix" select="'    '" />
      </xsl:call-template>
    </xsl:if>
    
    <xsl:text>&#xA;</xsl:text>
  </xsl:template>

  <xsl:template match="g:report" mode="results">
    <xsl:variable name="testCases" select="g:testPackageRun/g:testStepRun/descendant-or-self::g:testStepRun[g:testStep/@isTestCase='true']" />
    
    <xsl:variable name="passed" select="$testCases[g:result/g:outcome/@status='passed']" />
    <xsl:variable name="failed" select="$testCases[g:result/g:outcome/@status='failed']" />
    <xsl:variable name="inconclusive" select="$testCases[g:result/g:outcome/@status='inconclusive']" />
    <xsl:variable name="skipped" select="$testCases[g:result/g:outcome/@status='skipped']" />

    <xsl:if test="$show-passed-tests and $passed">
      <xsl:text>* Passed:&#xA;&#xA;</xsl:text>
      <xsl:apply-templates select="$passed" />
      <xsl:text>&#xA;</xsl:text>
    </xsl:if>

    <xsl:if test="$show-failed-tests and $failed">
      <xsl:text>* Failed:&#xA;&#xA;</xsl:text>
      <xsl:apply-templates select="$failed" />
      <xsl:text>&#xA;</xsl:text>
    </xsl:if>
    
    <xsl:if test="$show-inconclusive-tests and $inconclusive">
      <xsl:text>* Inconclusive:&#xA;&#xA;</xsl:text>
      <xsl:apply-templates select="$inconclusive" />
      <xsl:text>&#xA;</xsl:text>
    </xsl:if>
    
    <xsl:if test="$show-skipped-tests and $skipped">
      <xsl:text>* Skipped:&#xA;&#xA;</xsl:text>
      <xsl:apply-templates select="$skipped" />
      <xsl:text>&#xA;</xsl:text>
    </xsl:if>
	</xsl:template>
  
  <xsl:template match="g:testStepRun">
    <xsl:variable name="testId" select="g:testStep/@testId" />
    <xsl:variable name="test" select="//g:test[@id=$testId]" />

    <xsl:text>[</xsl:text>
    <xsl:value-of select="$test/g:metadata/g:entry[@key='TestKind']/g:value" />
    <xsl:text>] </xsl:text>
    <xsl:value-of select="g:testStep/@fullName" />
    <xsl:text>&#xA;</xsl:text>
    <xsl:apply-templates select="g:executionLog" />
    <xsl:text>&#xA;</xsl:text>

    <xsl:apply-templates select="g:children/g:testStepRun" />
  </xsl:template>

  <xsl:template match="g:executionLog">
    <xsl:apply-templates select="g:streams" />
  </xsl:template>

  <xsl:template match="g:streams">
    <xsl:apply-templates select="g:stream" />
  </xsl:template>
  
  <xsl:template match="g:stream">
    <xsl:param name="prefix" select="'  '" />

    <xsl:value-of select="$prefix"/>
    <xsl:text>&lt;Stream: </xsl:text>
    <xsl:value-of select="@name" />
    <xsl:text>&gt;&#xA;</xsl:text>
    <xsl:apply-templates select="g:body">
      <xsl:with-param name="prefix" select="concat($prefix, '  ')" />
    </xsl:apply-templates>
    <xsl:value-of select="$prefix"/>
    <xsl:text>&lt;End Stream&gt;&#xA;</xsl:text>
  </xsl:template>
  
  <xsl:template match="g:body">
    <xsl:param name="prefix" select="''" />

    <xsl:apply-templates select="g:contents">
      <xsl:with-param name="prefix" select="$prefix" />
    </xsl:apply-templates>
  </xsl:template>

  <xsl:template match="g:contents">
    <xsl:param name="prefix" select="''"  />
    
    <xsl:apply-templates select="child::node()[self::g:text or self::g:section or self::g:embed]">
      <xsl:with-param name="prefix" select="$prefix" />
    </xsl:apply-templates>
  </xsl:template>

  <xsl:template match="g:text">
    <xsl:param name="prefix" select="''"  />
    
    <xsl:call-template name="indent">
      <xsl:with-param name="text" select="text()" />
      <xsl:with-param name="prefix" select="$prefix" />
    </xsl:call-template>
  </xsl:template>

  <xsl:template match="g:section">
    <xsl:param name="prefix" select="''"  />
    
    <xsl:value-of select="$prefix"/>
    <xsl:text>&lt;Section: </xsl:text>
    <xsl:value-of select="@name" />
    <xsl:text>&gt;&#xA;</xsl:text>
    <xsl:apply-templates select="g:contents">
      <xsl:with-param name="prefix" select="concat($prefix, '  ')" />
    </xsl:apply-templates>
    <xsl:value-of select="$prefix"/>
    <xsl:text>&lt;End Section&gt;&#xA;</xsl:text>
  </xsl:template>

  <xsl:template match="g:embed">
    <xsl:param name="prefix" select="''"  />
    
    <xsl:value-of select="$prefix"/>
    <xsl:text>&lt;Attachment: </xsl:text>
    <xsl:value-of select="@attachmentName"/>
    <xsl:text>&gt;&#xA;</xsl:text>
  </xsl:template>

  <xsl:template match="*">
  </xsl:template>
  
  <!-- Include the common report template -->
  <xsl:include href="Gallio-Report.common.xsl" />  
</xsl:stylesheet>